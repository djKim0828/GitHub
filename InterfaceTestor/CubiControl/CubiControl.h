
// CubiControl.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CCubiControlApp:
// See CubiControl.cpp for the implementation of this class
//

class CCubiControlApp : public CWinApp
{
public:
	CCubiControlApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CCubiControlApp theApp;