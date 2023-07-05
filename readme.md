# Shimmering Unity

Shimmer integration into Unity.

## Usage

TBD

## Knowledge
### Download Consensys software
https://shimmersensing.com/support/wireless-sensor-networks-download/ >> Consensys V1.6.0 (64bit)
![image](https://github.com/jemmec/shimmering-unity/assets/15023431/f930b24b-e3af-4544-8ddd-0dfbaf674d35)

### Firmware manual & User guide
1. Manual: https://shimmersensing.com/wp-content/docs/support/documentation/LogAndStream_for_Shimmer3_Firmware_User_Manual_rev0.11a.pdf
2. User guide: https://shimmersensing.com/wp-content/docs/support/documentation/Consensys_User_Guide_rev1.6a.pdf

### Lighting Indication
![image](https://github.com/jemmec/shimmering-unity/assets/15023431/b65621bb-31c0-4846-b4d0-6b94c51f92a6)
![image](https://github.com/jemmec/shimmering-unity/assets/15023431/11cbc5a6-003e-493f-91e9-30620cde09d2)

### Manually installing ShimmerAPI into Unity

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
