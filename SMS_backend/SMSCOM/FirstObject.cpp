// FirstObject.cpp: CFirstObject 的实现

#include "pch.h"
#include "FirstObject.h"


// CFirstObject



STDMETHODIMP CFirstObject::Default(BSTR* a1)
{
    // TODO: 在此处添加实现代码
    MessageBox(NULL, L"test3", L"test", MB_OK);

    *a1 = ::SysAllocString(L"数据库出错，请查询。");

    return S_OK;
}
