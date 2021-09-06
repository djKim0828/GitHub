
// CubiControlDlg.h : header file
//

#pragma once


// CCubiControlDlg dialog
class CCubiControlDlg : public CDialogEx
{
// Construction
public:
	CCubiControlDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_CUBICONTROL_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);

	void CommandStart(CString parameter); //1
	void CommandConnect(CString parameter); //2
	void CommandDisconnect(CString parameter); //3
	void CommandGetStatus(CString parameter); //4
	void CommandPrepareScan(CString parameter);	 //5
	void CommandGrapStart(CString parameter); //6
	void CommandGrapStop(CString parameter); //7
	void CommandLaserOnOff(CString parameter); //8
	void CommandLedOnOff(CString parameter); //9
	void CommandScanStart(CString parameter); //10
	void CommandScanStop(CString parameter); //11
	void CommandSave(CString parameter); //12
	void CommandSaveParameter(CString parameter); //13
	void CommandLoadParameter(CString parameter); //14
	void CommandZCali(CString parameter); //15

	void CommandResult(CString parameter); //15

	int isConnect;

	HWND mainAppHandel;
	CString memoryKeyName;
};
