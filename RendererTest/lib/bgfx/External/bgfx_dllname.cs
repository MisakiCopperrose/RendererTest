/*
 * Copyright 2011-2022 Branimir Karadzic. All rights reserved.
 * License: https://github.com/bkaradzic/bgfx/blob/master/LICENSE
 */

/*
 *
 * AUTO GENERATED! DO NOT EDIT!
 *
 * Include this file in your build if you want to use the default DllImport
 * names of bgfx.dll and bgfx_debug.dll.  Otherwise, define your own
 * partial class like the below with a const DllName for your use.
 *
 */

namespace Bgfx
{
    public static partial class bgfx
    {
#if DEBUG
        const string DllName = "\\bgfx\\External\\macOS\\arm64\\libbgfx-shared-libDebug.dylib";
#else
       const string DllName = "\\bgfx\\External\\macOS\\arm64\\libbgfx-shared-libRelease.dylib";
#endif
    }
}