
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

			if (wParam == WM_START)
			{
				//AfxMessageBox(_T("WM_START"));

				CString strData = (LPCTSTR)p->lpData;				
				AfxExtractSubString(arStrArgs[0], strData, 0, '#');								
				int handle = _ttoi(arStrArgs[0]);

				AfxExtractSubString(arStrArgs[1], strData, 1, '#');

				
				
				HANDLE hMemoryMap;
				LPBYTE pMemoryMap;

				hMemoryMap = OpenFileMapping(FILE_MAP_ALL_ACCESS, NULL, _T(arStrArgs[1]));

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
				
				AfxExtractSubString(arStrArgs[2], strData, 2, '#');
				int value = _ttoi(arStrArgs[2]);

				for (int i = 0; i < size; i++)
				{
					msh_ImgBuffer[i] = value;
				}

				memset(pMemoryMap, 0, size);	
				memcpy(pMemoryMap, msh_ImgBuffer, size);

				// 바로 메세지를 전달는 샘플 Code입니다.
				// SendMessage를 위하여 Handle 은 별도 멤버로 선언하여 사용 바랍니다.				
				HWND mainAppHandel = (HWND)handle;
				::SendMessage(mainAppHandel, WM_CUBIEVENT, handle, value);

				if (pMemoryMap)
				{
					UnmapViewOfFile(pMemoryMap);
				}

				if (hMemoryMap)
				{
					CloseHandle(hMemoryMap);
				}
			}

			break;
		}
		default:
			break;
	}

	return CDialog::WindowProc(message, wParam, lParam);
}

