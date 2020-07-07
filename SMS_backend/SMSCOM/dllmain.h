// dllmain.h: 模块类的声明。

class CSMSCOMModule : public ATL::CAtlDllModuleT< CSMSCOMModule >
{
public :
	DECLARE_LIBID(LIBID_SMSCOMLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_SMSCOM, "{f2b107d7-0daf-4126-8f03-b85e7a3f2149}")
};

extern class CSMSCOMModule _AtlModule;
