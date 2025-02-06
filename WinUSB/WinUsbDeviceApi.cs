using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace WinUsb
{
    /// <summary>
    ///  These declarations are translated from the C declarations in various files
    ///  in the Windows DDK. The files are:
    ///  
    ///  winddk\6001\inc\api\usb.h
    ///  winddk\6001\inc\api\usb100.h
    ///  winddk\6001\inc\api\winusbio.h
    ///  
    ///  (your home directory and release number may vary)
    /// <summary>
    sealed internal partial class WinUsbDevice
    {
        internal const uint DEVICE_SPEED = 1;
        internal const byte USB_ENDPOINT_DIRECTION_MASK = 0X80;

        internal enum POLICY_TYPE
        {
            SHORT_PACKET_TERMINATE = 1,
            AUTO_CLEAR_STALL,
            PIPE_TRANSFER_TIMEOUT,
            IGNORE_SHORT_PACKETS,
            ALLOW_PARTIAL_READS,
            AUTO_FLUSH,
            RAW_IO,
        }

        internal enum USB_DEVICE_SPEED
        {
            UsbLowSpeed = 1,
            UsbFullSpeed,
            UsbHighSpeed,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct USB_CONFIGURATION_DESCRIPTOR
        {
            internal byte bLength;
            internal byte bDescriptorType;
            internal ushort wTotalLength;
            internal byte bNumInterfaces;
            internal byte bConfigurationValue;
            internal byte iConfiguration;
            internal byte bmAttributes;
            internal byte MaxPower;
        }
    }

    sealed internal class NativeMethods
    {
        internal enum USBD_PIPE_TYPE
        {
            UsbdPipeTypeControl,
            UsbdPipeTypeIsochronous,
            UsbdPipeTypeBulk,
            UsbdPipeTypeInterrupt,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINUSB_PIPE_INFORMATION
        {
            internal USBD_PIPE_TYPE PipeType;
            internal byte PipeId;
            internal ushort MaximumPacketSize;
            internal byte Interval;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct WINUSB_SETUP_PACKET
        {
            internal byte RequestType;
            internal byte Request;
            internal ushort Value;
            internal ushort Index;
            internal ushort Length;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct USB_INTERFACE_DESCRIPTOR
        {
            internal byte bLength;
            internal byte bDescriptorType;
            internal byte bInterfaceNumber;
            internal byte bAlternateSetting;
            internal byte bNumEndpoints;
            internal byte bInterfaceClass;
            internal byte bInterfaceSubClass;
            internal byte bInterfaceProtocol;
            internal byte iInterface;
        }

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_ControlTransfer(IntPtr InterfaceHandle, WINUSB_SETUP_PACKET SetupPacket, byte[] Buffer, uint BufferLength, ref uint LengthTransferred, IntPtr Overlapped);

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_Free(IntPtr InterfaceHandle);

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_Initialize(SafeFileHandle DeviceHandle, ref IntPtr InterfaceHandle);

        //  Use this declaration to retrieve DEVICE_SPEED (the only currently defined InformationType).

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_QueryDeviceInformation(IntPtr InterfaceHandle, uint InformationType, ref uint BufferLength, ref byte Buffer);

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_QueryInterfaceSettings(IntPtr InterfaceHandle, byte AlternateInterfaceNumber, ref USB_INTERFACE_DESCRIPTOR UsbAltInterfaceDescriptor);

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_QueryPipe(IntPtr InterfaceHandle, byte AlternateInterfaceNumber, byte PipeIndex, ref WINUSB_PIPE_INFORMATION PipeInformation);

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_ReadPipe(IntPtr InterfaceHandle, byte PipeID, byte[] Buffer, uint BufferLength, ref uint LengthTransferred, IntPtr Overlapped);

        //  Two declarations for WinUsb_SetPipePolicy. 
        //  Use this one when the returned Value is a byte (all except PIPE_TRANSFER_TIMEOUT):

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_SetPipePolicy(IntPtr InterfaceHandle, byte PipeID, uint PolicyType, uint ValueLength, ref byte Value);

        //  Use this alias when the returned Value is a uint (PIPE_TRANSFER_TIMEOUT only):

        [DllImport("winusb.dll", SetLastError = true, EntryPoint = "WinUsb_SetPipePolicy")]
        internal static extern bool WinUsb_SetPipePolicy1(IntPtr InterfaceHandle, byte PipeID, uint PolicyType, uint ValueLength, ref uint Value);

        [DllImport("winusb.dll", SetLastError = true)]
        internal static extern bool WinUsb_WritePipe(IntPtr InterfaceHandle, byte PipeID, byte[] Buffer, uint BufferLength, ref uint LengthTransferred, IntPtr Overlapped);
    }
}