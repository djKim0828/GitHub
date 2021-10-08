using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.Lib.Common
{
    /// <summary>
    /// Thread로 함수 실행하는 클래스
    /// </summary>
    public class TaskExecutor
    {
        private int _isTaskBusy = -1;
        private int _exceptionError = -2;

        public Task<int> IsCall { get; private set; }

        ~TaskExecutor()
        {
            if (IsCall != null)
            {
                IsCall.Dispose();
                IsCall = null;
            }
        }

        public async Task<int> Call(Func<object[], int> callbackFunc, params object[] args)
        {
            int result = _isTaskBusy;
            try
            {
                if (IsCall != null)
                {
                    if (IsCall.Status == TaskStatus.Running)
                        return _isTaskBusy;
                }
                Logger.Debug("TaskRun", $"{Emu.CurrentTime}", nameof(callbackFunc.Method));
                IsCall = Task.Run(() => callbackFunc(args));
                result = await IsCall;
                Logger.Debug("TaskStop", $"{Emu.CurrentTime}", nameof(callbackFunc.Method));
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                Trace.Assert(false, $"{Emu.CurrentTime} - {callbackFunc.Method}", ex.Message);
                result = _exceptionError;
            }
            finally
            {
                IsCall.Dispose();
                IsCall = null;
            }

            return result;
        }
    }

    /// <summary>
    /// Timeout 확인하는 클래스.
    /// </summary>
    public class TaskTimeout
    {
        private int _timeout = -3;
        private int _nullAction = -4;
        private Action _action;

        public TaskTimeout(Action action)
        {
            _action = action;
        }

        public int Check(int timeout)
        {
            if (_action == null)
                return _nullAction;

            IAsyncResult async_result = _action.BeginInvoke(null, null);
            if (async_result.AsyncWaitHandle.WaitOne(timeout))
                return 0;
            else
            {
                Logger.Debug(nameof(TaskTimeout), $"{Emu.CurrentTime}", $"{_action.Method}", $"Timeout : {timeout}");
                Trace.Assert(false, $"{Emu.CurrentTime} - {nameof(TaskTimeout)}-{_action.Method}");
                return _timeout;
            }
        }
    }

    /// <summary>
    /// <see cref="TaskExecutor"/>를 Dictonary로 관리하는 클래스.
    /// 일반 함수를 Thread에서 실행하기 위해 만들었음.
    /// 아래의 Tasks Class 사용 예제 참고.
    /// </summary>
    public class Tasks
    {
        private static readonly Lazy<Tasks> _inst = new Lazy<Tasks>(() => new Tasks());

        public static Tasks Inst
        {
            get { return _inst.Value; }
        }

        public int Count { get; private set; } = 256;

        private static Dictionary<int, TaskExecutor> _calls = null;

        public TaskExecutor this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    return null;

                return _calls[index];
            }
        }

        /// <summary>
        /// get idle index
        /// </summary>
        public int IdleIndex
        {
            get
            {
                return GetIdleIndex();
            }
        }

        public Tasks()
        {
            Initialize();
        }

        private void Initialize()
        {
            _calls = new Dictionary<int, TaskExecutor>();
            for (int i = 0; i < Count; i++)
                _calls.Add(i, new TaskExecutor());
        }

        private int GetIdleIndex()
        {
            int index = 0;
            foreach (var item in _calls)
            {
                if (item.Value.IsCall == null)
                    break;
                if (item.Value.IsCall.Status != TaskStatus.Running)
                    break;
                index++;
            }
            return index;
        }

        public static int UpdateCtrlAfterRead(Func<object, int> readFunc, Func<object, int> beCallbackUpdateCtrlFunc, object arg)
        {
            int ret = 0;
            Task<int>.Factory.StartNew(() => readFunc(arg)).ContinueWith(o =>
            {
                ret = o.Result;
                beCallbackUpdateCtrlFunc(ret);
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return ret;

            #region example for using UpdateCtrlAfterRead()
            //private void btnCallback_Click(object sender, EventArgs e)
            //{
            //    ServiceClass sc = new ServiceClass();
            //    int ret = UpdateCtrlAfterRead(Read, UpdateCtrl, Convert.ToInt32(txtCallbackResult.Text));
            //}
            //public int Read(object value)
            //{
            //    Thread.Sleep(5000);
            //    return (int)value * 10;
            //}
            //private int UpdateCtrl(object returnValue)
            //{
            //    txtCallbackResult.Text = returnValue.ToString();
            //    return 0;
            //}
            #endregion
        }
    }

    /* Tasks Class 사용 예제.
       // 함수 호출 (사용)
        private async void btnCallback_Click(object sender, EventArgs e)
        {
            if (Tasks.Inst[0].IsCall != null)
                return;

            int ret = 0;
            int value = Convert.ToInt32(txtCallbackResult.Text);

            ret = await Tasks.Inst[0].Call(LongCalcAsync, value, "asdf");
            txtCallbackResult.Text = ret.ToString();
        }
		// Thread로 돌아야 하는 함수.
        public int LongCalcAsync(params object [] data)
        {
		    // 반드시 아래 기능 구현 할 것!
		    // 1st check interlock
			// 2nd execute command
			// 3rd check result and timeout

            if (data.Length == 0)
                data = new object[1] { 5 };

            // result check routine
            int result = 0;
            Action command_check = () =>
            {
                for (int i = 0; i < (int)data[0]; i++)
                {
                    result += i;
                    Thread.Sleep(100);
                }
            };

			// timeout check
            int timeout_result = new TaskTimeout(command_check).Check(2000);
            if (timeout_result  != 0)
                return timeout_result;

            return result;
        }
     */
}
