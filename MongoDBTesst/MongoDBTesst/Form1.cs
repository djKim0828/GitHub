using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MongoDBTesst
{
    public partial class Form1 : Form
    {
        #region Fields

        private MongoCollection<Customer> _table;
        private MongoDatabase _testdb;

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Customer cust1 = new Customer { name = txtName.Text, age = txtAge.Text };
            _table.Insert(cust1);

            btnSelectAll_Click(null, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dgvList.DataSource = null;

            IMongoQuery query = Query<Customer>.Where(d => d.name == txtName.Text && d.age == txtAge.Text);
            var result = _table.Find(query).SingleOrDefault();
            if (result != null)
            {
                List<Customer> result2 = new List<Customer>();
                result2.Add(result);
                dgvList.DataSource = result2;
            } // else
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<Customer> result2 = _table.FindAll().ToList<Customer>();
            dgvList.DataSource = result2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = "mongodb://localhost";

            // MongoClient 클라이언트 객체 생성
            MongoClient cli = new MongoClient(connString);

            // testdb 라는 데이타베이스 가져오기
            // 만약 해당 DB 없으면 Collection 생성시 새로 생성함
            _testdb = cli.GetServer().GetDatabase("testdb");

            if (_testdb == null)
            {
                WriteLog("Failed db connect");
            } // else

            _table = _testdb.GetCollection<Customer>("testdb");

            if (_table == null)
            {
                WriteLog("Failed db table connect");
            } // else

            WriteLog(" DB & TAble Connecting");

            //IMongoQuery query = Query.EQ("name", "kildong");
            ////IMongoQuery query = Query.EQ("_id", id);
            //var result = _table.Find(query).SingleOrDefault();
            //if (result != null)
            //{
            //    Console.WriteLine(result.ToString());
            //}
        }

        private void WriteLog(string message)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + message + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        #endregion Methods

        #region Classes

        private class Customer
        {
            #region Properties

            public string age { get; set; }

            // 이 Id는 MongoDB Collection의 _id 컬럼과 매칭됨
            // (예외적인 Naming Convention)
            public ObjectId Id { get; set; }

            public string name { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}