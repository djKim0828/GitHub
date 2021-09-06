
// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#ifndef VC_EXTRALEAN
#define VC_EXTRALEAN            // Exclude rarely-used stuff from Windows headers
#endif

#include "targetver.h"

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS      // some CString constructors will be explicit

// turns off MFC's hiding of some common and often safely ignored warning messages
#define _AFX_ALL_WARNINGS

#include <afxwin.h>         // MFC core and standard components
#include <afxext.h>         // MFC extensions


#include <afxdisp.h>        // MFC Automation classes



#ifndef _AFX_NO_OLE_SUPPORT
#include <afxdtctl.h>           // MFC support for Internet Explorer 4 Common Controls
#endif
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>             // MFC support for Windows Common Controls
#endif // _AFX_NO_AFXCMN_SUPPORT

#include <afxcontrolbars.h>     // MFC support for ribbons and control bars

#ifdef _UNICODE
#if defined _M_IX86
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='x86' publicKeyToken='6595b64144ccf1df' language='*'\"")
#elif defined _M_X64
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='amd64' publicKeyToken='6595b64144ccf1df' language='*'\"")
#else
#pragma comment(linker,"/manifestdependency:\"type='win32' name='Microsoft.Windows.Common-Controls' version='6.0.0.0' processorArchitecture='*' publicKeyToken='6595b64144ccf1df' language='*'\"")
#endif
#endif

// ENC -> Cubixel
#define START			WM_USER + 1
#define CONNECT			WM_USER + 2
#define DISCONNECT		WM_USER + 3
#define GET_STATUS		WM_USER + 4
#define PREPARE_SCAN	WM_USER + 5
#define LASER_ON_OFF	WM_USER + 6
#define LED_ON_OFF		WM_USER + 7
#define GRAP_START		WM_USER + 8
#define GRAP_STOP		WM_USER + 9
#define SCAN_START		WM_USER + 10
#define SCAN_STOP		WM_USER + 11
#define SAVE			WM_USER + 12
#define SAVE_PARAMETERS WM_USER + 13
#define LOAD_PARAMETERS	WM_USER + 14
#define SAVE_ZCALPARAM	WM_USER + 15

// Cubixel -> ENC
#define RETURN	WM_USER + 240
#define RETURN_STATUS	WM_USER + 241
#define RETURN_VALUE	WM_USER + 242
#define CAPTURE_COMPLETE	WM_USER + 243
#define ERROR_EVENT	WM_USER + 244



