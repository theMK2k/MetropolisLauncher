#include <Constants.au3>

;
; Metropolis Launcher AutoIt Support v1.0
; based on AutoIt Version 3
;
; Script Function:
;	This is a template script
;	Use it as the starting point for developing your own script

Global $mlEMUDIR           ; the directory of the emulator
Global $mlEMUEXE           ; the executable of the emulator
Global $mlEMUFULLPATH      ; the full path (emudir \ emuexe) of the emulator
Global $mlLISTFILE         ; full path to the generated list file
Global $mlLIBRETROCORE     ; name of the libretro core
Global $mlCONFIGFILE       ; full path to a generated config file (ScummVM, DOSBox)

Global $mlNUMBEROFROMS     ; total number of rom files/media

Global $mlROMDIR[99]       ; directory of each rom files/media
Global $mlROMFILE[99]      ; filename of each rom file/media (for ScummVM: gameid)
Global $mlROMEXTENSION[99] ; file extension of each rom file/media
Global $mlROMFULLPATH[99]  ; full path of each rom file/media

; ### Meta Data Fields ###

Global $mlGAMEID           ; internal ID of the game
Global $mlGAMENAME         ; name of the game as shown in Metropolis Launcher
Global $mlREGIONS          ; region info, e.g. "Europe", "USA, Japan"
Global $mlLANGUAGES        ; language info, e.g. "(EN)", "DE, FR"
Global $mlMOBYRANK         ; mobygames rank
Global $mlMOBYSCORE        ; mobygames score
Global $mlYEAR             ; release year
Global $mlPUBLISHER        ; name of the publisher
Global $mlDEVELOPER        ; name of the developer
Global $mlMINPLAYERS       ; minimum number of players
Global $mlMAXPLAYERS       ; maximum number of players

; ### ML INJECT START ###
; this section may be filled for testing purposes, but don't remove the INJECT START and END tags!

$mlEMUDIR = "c:\Emulators\MyEmulator"
$mlEMUEXE = "MyEmulator.exe"

$mlNUMBEROFROMS = 1

$mlROMDIR[0] = "c:\Roms"
$mlROMFILE[0] = "some_rom.bin"
$mlROMEXTENSION[0] = "bin"
$mlROMFULLPATH[0] = "c:\Roms\some_rom.bin"

; ### ML INJECT END ###

; ### Start here with your code ###

; just MsgBox some variable contents
MsgBox($MB_SYSTEMMODAL, "Accessing some data", $mlEMUDIR & @CRLF & $mlEMUEXE & @CRLF & $mlROMFULLPATH[0] & @CRLF & "EOF")
