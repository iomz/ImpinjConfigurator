ImpinjConfigurator
==============
Just config the Impinj readers with OctaneSDK to set ROReport destination by deploying LLRP from this application host.

Synopsis
==============
Edit SolutionConstants.cs first to specify the reader name or its IP address.

```sh
git clone https://github.com/iomz/ImpinjConfigurator.git
# Copy OctaneSdk_.NET_2_0_2_240/lib to ImpinjConfigurator/lib
mdtool build ImpinjConfigurator.csproj
mono bin/Debug/ImpinjConfigurator.exe
```

Reference
==============
* https://support.impinj.com/hc/en-us/articles/202755268-Octane-SDK#net 
