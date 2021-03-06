
// CubiControlDlg.cpp : implementation file
//

#include "stdafx.h"
#include "CubiControl.h"
#include "CubiControlDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CCubiControlDlg dialog



CCubiControlDlg::CCubiControlDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_CUBICONTROL_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CCubiControlDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CCubiControlDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CCubiControlDlg message handlers

BOOL CCubiControlDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CCubiControlDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CCubiControlDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CCubiControlDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

LRESULT CCubiControlDlg::WindowProc(UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
		case WM_COPYDATA:
		{
			CString arStrArgs[7];

			COPYDATASTRUCT *p = (COPYDATASTRUCT*)lParam;

			CString strData, temp;
			strData.Format(_T("%s"), (char *)lParam);			

			strData = (LPCTSTR)p->lpData;

			switch (wParam)
			{
				case START:
					CommandStart(strData);				
					break;
				case CONNECT:
					CommandConnect(strData);
					break;
				case DISCONNECT:
					CommandDisconnect(strData);
					break;
				case GET_STATUS:
					CommandGetStatus(strData);
					break;
				case GRAP_START:
					CommandGrapStart(strData);
					break;
				case LASER_ON_OFF:
					CommandLaserOnOff(strData);
					break;
				default:
					CommandResult(strData);
					break;;
			}			

			break;
		}
		default:
			break;
	}
	return CDialog::WindowProc(message, wParam, lParam);
}

void CCubiControlDlg::CommandStart(CString parameter)
{
	CString arStrArgs[7];
	AfxExtractSubString(arStrArgs[0], parameter, 0, '#');
	int handle = _ttoi(arStrArgs[0]);
	mainAppHandel = (HWND)handle;

	AfxExtractSubString(arStrArgs[1], parameter, 1, '#');
	memoryKeyName = arStrArgs[1];

	isConnect = 0;

	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandGrapStart(CString parameter)
{
	CString arStrArgs[7];
	AfxExtractSubString(arStrArgs[0], parameter, 0, '#');

	HANDLE hMemoryMap;
	LPBYTE pMemoryMap;

	hMemoryMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, NULL, _T(memoryKeyName));

	/*if (!hMemoryMap)
	{
	return ERROR_ORI_OPENFILEMAPPING_FAIL;
	}*/

	pMemoryMap = (LPBYTE)MapViewOfFile(hMemoryMap, FILE_MAP_ALL_ACCESS, 0, 0, 0);

	if (!pMemoryMap)
	{
		CloseHandle(hMemoryMap);
		//return ERROR_ORI_MAPVIEWOFFILE_FAIL;
	}

	int w = 2000;
	int h = 2000;
	int size = w * h;

	byte *msh_ImgBuffer;
	msh_ImgBuffer = new byte[size];

	AfxExtractSubString(arStrArgs[0], parameter, 0, '#');
	int value = _ttoi(arStrArgs[0]);

	for (int i = 0; i < size; i++)
	{
		msh_ImgBuffer[i] = value;
	}

	memset(pMemoryMap, 0, size);
	memcpy(pMemoryMap, msh_ImgBuffer, size);
	
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);

	Sleep(10);
	
	::SendMessage(mainAppHandel, RETURN, CAPTURE_COMPLETE, value);

	

	if (pMemoryMap)
	{
		UnmapViewOfFile(pMemoryMap);
	}

	if (hMemoryMap)
	{
		CloseHandle(hMemoryMap);
	}
}

void CCubiControlDlg::CommandConnect(CString parameter)
{	
	isConnect = 1;

	Sleep(200);	
	
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandDisconnect(CString parameter)
{
	isConnect = 0;

	Sleep(100);

	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandGetStatus(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_STATUS, isConnect);
}

void CCubiControlDlg::CommandPrepareScan(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandGrapStop(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandLaserOnOff(CString parameter)
{
	CString arStrArgs[7];
	AfxExtractSubString(arStrArgs[0], parameter, 0, '#');
	int OnOff = _ttoi(arStrArgs[0]);

	if (OnOff == 1)
	{
		Sleep(1000);
	} // else	

	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandLedOnOff(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandScanStart(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandScanStop(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandSave(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandSaveParameter(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandLoadParameter(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandZCali(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}

void CCubiControlDlg::CommandResult(CString parameter)
{
	::SendMessage(mainAppHandel, RETURN, RETURN_VALUE, 1);
}



















