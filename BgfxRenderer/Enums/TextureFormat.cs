using Bgfx;

namespace BgfxRenderer.Enums;

public enum TextureFormat
{
    /// <summary>
    /// Compressed DXT1 R5G6B5A1
    /// </summary>
    BC1 = bgfx.TextureFormat.BC1,

    /// <summary>
    /// Compressed DXT3 R5G6B5A4
    /// </summary>
    BC2 = bgfx.TextureFormat.BC2,

    /// <summary>
    /// Compressed DXT5 R5G6B5A8
    /// </summary>
    BC3 = bgfx.TextureFormat.BC3,

    /// <summary>
    /// Compressed LATC1/ATI1 R8
    /// </summary>
    BC4 = bgfx.TextureFormat.BC4,

    /// <summary>
    /// Compressed LATC2/ATI2 RG8
    /// </summary>
    BC5 = bgfx.TextureFormat.BC4,

    /// <summary>
    /// Compressed BC6H RGB16F
    /// </summary>
    BC6H = bgfx.TextureFormat.BC6H,

    /// <summary>
    /// Compressed BC7 RGB 4-7 bits per color channel, 0-8 bits alpha
    /// </summary>
    BC7 = bgfx.TextureFormat.BC7,

    /// <summary>
    /// Compressed ETC1 RGB8
    /// </summary>
    ETC1 = bgfx.TextureFormat.ETC1,

    /// <summary>
    /// Compressed ETC2 RGB8
    /// </summary>
    ETC2 = bgfx.TextureFormat.ETC2,

    /// <summary>
    /// Compressed ETC2 RGBA8
    /// </summary>
    ETC2A = bgfx.TextureFormat.ETC2A,

    /// <summary>
    /// Compressed ETC2 RGB8A1
    /// </summary>
    ETC2A1 = bgfx.TextureFormat.ETC2A1,

    /// <summary>
    /// Compressed PVRTC1 RGB 2BPP
    /// </summary>
    PTC12 = bgfx.TextureFormat.PTC12,

    /// <summary>
    /// Compressed PVRTC1 RGB 4BPP
    /// </summary>
    PTC14 = bgfx.TextureFormat.PTC14,

    /// <summary>
    /// Compressed PVRTC1 RGBA 2BPP
    /// </summary>
    PTC12A = bgfx.TextureFormat.PTC12A,

    /// <summary>
    /// Compressed PVRTC1 RGBA 4BPP
    /// </summary>
    PTC14A = bgfx.TextureFormat.PTC14A,

    /// <summary>
    /// Compressed PVRTC2 RGBA 2BPP
    /// </summary>
    PTC22 = bgfx.TextureFormat.PTC22,

    /// <summary>
    /// Compressed PVRTC2 RGBA 4BPP
    /// </summary>
    PTC24 = bgfx.TextureFormat.PTC24,

    /// <summary>
    /// Compressed ATC RGB 4BPP
    /// </summary>
    ATC = bgfx.TextureFormat.ATC,

    /// <summary>
    /// Compressed ATCE RGBA 8 BPP explicit alpha
    /// </summary>
    ATCE = bgfx.TextureFormat.ATCE,

    /// <summary>
    /// Compressed ATCI RGBA 8 BPP interpolated alpha
    /// </summary>
    ATCI = bgfx.TextureFormat.ATCI,

    /// <summary>
    /// Compressed ASTC 4x4 8.0 BPP
    /// </summary>
    ASTC4x4 = bgfx.TextureFormat.ASTC4x4,

    /// <summary>
    /// Compressed ASTC 5x5 5.12 BPP
    /// </summary>
    ASTC5x5 = bgfx.TextureFormat.ASTC5x5,

    /// <summary>
    /// Compressed ASTC 6x6 3.56 BPP
    /// </summary>
    ASTC6x6 = bgfx.TextureFormat.ASTC6x6,

    /// <summary>
    /// Compressed ASTC 8x5 3.20 BPP
    /// </summary>
    ASTC8x5 = bgfx.TextureFormat.ASTC8x5,

    /// <summary>
    /// Compressed ASTC 8x6 2.67 BPP
    /// </summary>
    ASTC8x6 = bgfx.TextureFormat.ASTC8x6,

    /// <summary>
    /// Compressed ASTC 10x5 2.56 BPP
    /// </summary>
    ASTC10x5 = bgfx.TextureFormat.ASTC10x5,

    Unknown,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R1 = bgfx.TextureFormat.R1,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    A8 = bgfx.TextureFormat.A8,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R8 = bgfx.TextureFormat.R8,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R8I = bgfx.TextureFormat.R8I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R8U = bgfx.TextureFormat.R8U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R8S = bgfx.TextureFormat.R8S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R16 = bgfx.TextureFormat.R16,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R16I = bgfx.TextureFormat.R16I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R16U = bgfx.TextureFormat.R16U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R16F = bgfx.TextureFormat.R16F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R16S = bgfx.TextureFormat.R16S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R32I = bgfx.TextureFormat.R32I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R32U = bgfx.TextureFormat.R32U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R32F = bgfx.TextureFormat.R32F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG8 = bgfx.TextureFormat.RG8,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG8I = bgfx.TextureFormat.RG8I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG8U = bgfx.TextureFormat.RG8U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG8S = bgfx.TextureFormat.RG8S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG16 = bgfx.TextureFormat.RG16,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG16I = bgfx.TextureFormat.RG16I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG16U = bgfx.TextureFormat.RG16U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG16F = bgfx.TextureFormat.RG16F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG16S = bgfx.TextureFormat.RG16S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG32I = bgfx.TextureFormat.RG32I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG32U = bgfx.TextureFormat.RG32U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG32F = bgfx.TextureFormat.RG32F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB8 = bgfx.TextureFormat.RGB8,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB8I = bgfx.TextureFormat.RGB8I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB8U = bgfx.TextureFormat.RGB8U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB8S = bgfx.TextureFormat.RGB8S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB9E5F = bgfx.TextureFormat.RGB9E5F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    BGRA8 = bgfx.TextureFormat.BGRA8,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA8 = bgfx.TextureFormat.RGBA8,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA8I = bgfx.TextureFormat.RGBA8I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA8U = bgfx.TextureFormat.RGBA8U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA8S = bgfx.TextureFormat.RGBA8S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA16 = bgfx.TextureFormat.RGBA16,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA16I = bgfx.TextureFormat.RGBA16I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA16U = bgfx.TextureFormat.RGBA16U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA16F = bgfx.TextureFormat.RGBA16F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA16S = bgfx.TextureFormat.RGBA16S,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA32I = bgfx.TextureFormat.RGBA32I,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA32U = bgfx.TextureFormat.RGBA32U,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA32F = bgfx.TextureFormat.RGBA32F,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    R5G6B5 = bgfx.TextureFormat.R5G6B5,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGBA4 = bgfx.TextureFormat.RGBA4,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB5A1 = bgfx.TextureFormat.RGB5A1,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RGB10A2 = bgfx.TextureFormat.RGB10A2,

    /// <summary>
    /// How to read:
    /// RGBA16S -> RGBA: components | 16: number of bits per component | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    RG11B10F = bgfx.TextureFormat.RG11B10F,

    UnknownDepth,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D16 = bgfx.TextureFormat.D16,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D24 = bgfx.TextureFormat.D24,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D24S8 = bgfx.TextureFormat.D24S8,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D32 = bgfx.TextureFormat.D32,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D16F = bgfx.TextureFormat.D16F,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D24F = bgfx.TextureFormat.D24F,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D32F = bgfx.TextureFormat.D32F,

    /// <summary>
    /// How to read:
    /// D16F -> D: depth component | 16: number of bits | S: [ ]unorm [F]float [S]norm [I]int [U]uint
    /// </summary>
    D0S8 = bgfx.TextureFormat.D0S8
}