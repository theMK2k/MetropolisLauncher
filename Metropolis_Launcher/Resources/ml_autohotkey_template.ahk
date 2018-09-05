;
; Metropolis Launcher AutoHotKey Support v1.0
; based on AutoHotKey Version 1.1.29.01
;
; Script Function:
;	This is a template script
;	Use it as the starting point for developing your own script

mlEMUDIR := ""          ; the directory of the emulator
mlEMUEXE := ""          ; the executable of the emulator
mlEMUFULLPATH := ""     ; the full path (emudir \ emuexe) of the emulator
mlLISTFILE := ""        ; full path to the generated list file
mlLIBRETROCORE := ""		; name of the libretro core
mlCONFIGFILE := ""      ; full path to a generated config file (ScummVM, DOSBox)

mlNUMBEROFROMS := 0     ; total number of rom files/media

; Metropolis Launcher will provide the following pseudo-arrays in the INJECT section:
; mlROMDIR              ; directory of each rom file/media
; mlROMFILE             ; filename of the rom file/media (for ScummVM: gameid)
; mlROMEXTENSION        ; file extension of the rom file/media
; mlROMFULLPATH         ; full path of the rom file/media

; ### Meta Data Fields ###

mlGAMEID := ""					; internal ID of the game
mlGAMENAME := ""        ; name of the game as shown in Metropolis Launcher
mlREGIONS := ""         ; region info, e.g. "Europe", "USA, Japan"
mlLANGUAGES := ""       ; language info, e.g. "(EN)", "DE, FR"
mlMOBYRANK := ""        ; mobygames rank
mlMOBYSCORE := ""       ; mobygames score
mlYEAR := ""            ; release year
mlPUBLISHER := ""       ; name of the publisher
mlDEVELOPER := ""       ; name of the developer
mlMINPLAYERS := ""      ; minimum number of players
mlMAXPLAYERS := ""      ; maximum number of players

; ### ML INJECT START ###
; this section may be filled for testing purposes, but don't remove the INJECT START and END tags!

mlEMUDIR := "c:\Emulators\MyEmulator"
mlEMUEXE := "MyEmulator.exe"

mlROMDIR0 := "c:\Roms"
mlROMFILE0 := "some_rom.bin"
mlROMEXTENSION0 := "bin"
mlROMFULLPATH0 := "c:\Roms\some_rom.bin"

; ### ML INJECT END ###

; ### Start here with your code ###

; just MsgBox some variable contents
MsgBox Accessing some data `r`n %mlEMUDIR% `r`n %mlEMUEXE% `r`n %mlROMFULLPATH0% `r`n EOF
