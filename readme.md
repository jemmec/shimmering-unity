# Shimmering Unity

Shimmer integration into Unity.

## Download software
https://shimmersensing.com/support/wireless-sensor-networks-download/ >> Consensys V1.6.0 (64bit)
![image](https://github.com/jemmec/shimmering-unity/assets/15023431/f930b24b-e3af-4544-8ddd-0dfbaf674d35)


## Installation Process

1. Clone the ShimmerAPI solution https://github.com/ShimmerEngineering/Shimmer-C-API

2. Build the Class Libraries for ShimmerAPI in Visual Studio.

3. Copy the correct version of following `.dll` files into unity `/Plugins` folder.

    - Google.Protobuf (netstandard2.0)
    - Grpc.Core.Api (netstandard2.0)
    - Grpc.Core (netstandard2.0)
    - MathNet.Numerics (netstandard2.0)
    - ShimmerAPI (netstandard2.0)
    - System.Runtime.CompilerServices.Unsafe (netstandard2.0)
    - System.IO.Ports (net 461)
    
4. Set API target to .Net Framework inside Unity Project Settings.

5. Create a script that connects to your shimmer device via the ShimmerAPI.

## Credits

The [ShimmerAPI](https://github.com/ShimmerEngineering/Shimmer-C-API) was create by [Shimmer](https://shimmersensing.com/)
