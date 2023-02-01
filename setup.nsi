; NSIS Installation Script
; Adapted from https://gist.github.com/zmilojko/3174804

!define PRODUCT_NAME "DavesSystemTuner"
!define PRODUCT_VERSION "1.0.0"
!define PRODUCT_PUBLISHER "Dave Rich"
!define PRODUCT_WEB_SITE "https://github.com/davidarich/davessystemtuner"

; Configuration constants
!define PUBLISH_OUTPUT_DIR "DavesSystemTuner\bin\Release\publish"

; Following constants (don't change)
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

!include "MUI.nsh"
!define MUI_ABORTWARNING
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Wizard pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "LICENSE.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup_${PRODUCT_NAME}_${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES\DavesSytemTuner"
ShowInstDetails show
ShowUnInstDetails show

; Install files to specified directory & desktop shortcut
Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  ; app executable
  File "${PUBLISH_OUTPUT_DIR}\DavesSystemTuner.exe"
  ; config (don't remove)
  File "${PUBLISH_OUTPUT_DIR}\DavesSystemTuner.runtimeconfig.json"
  ; libraries
  File "${PUBLISH_OUTPUT_DIR}\AeroWizard.dll"
  File "${PUBLISH_OUTPUT_DIR}\CommunityToolkit.Mvvm.dll"
  File "${PUBLISH_OUTPUT_DIR}\DavesSystemTuner.dll"
  File "${PUBLISH_OUTPUT_DIR}\GroupControls.dll"
  File "${PUBLISH_OUTPUT_DIR}\Microsoft.Win32.TaskScheduler.dll"
  File "${PUBLISH_OUTPUT_DIR}\Microsoft.Win32.TaskSchedulerEditor.dll"
  File "${PUBLISH_OUTPUT_DIR}\TimeSpan2.Core.dll"
  File "${PUBLISH_OUTPUT_DIR}\TimeSpan2.dll"

  ; Setup Desktop Shortcut
  ; todo: make shortcut optional
  CreateShortCut "$DESKTOP\${PRODUCT_NAME}.lnk" "$INSTDIR\DavesSystemTuner.exe"
SectionEnd

Section -Post
  ;Following lines will make uninstaller work - do not change anything, unless you really want to.
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "Application was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove Dave's System Tuner and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  ; app executable
  Delete "$INSTDIR\DavesSystemTuner.exe"
  ; config (don't remove)
  Delete "$INSTDIR\DavesSystemTuner.runtimeconfig.json"
  ; libraries
  Delete "$INSTDIR\AeroWizard.dll"
  Delete "$INSTDIR\CommunityToolkit.Mvvm.dll"
  Delete "$INSTDIR\DavesSystemTuner.dll"
  Delete "$INSTDIR\GroupControls.dll"
  Delete "$INSTDIR\Microsoft.Win32.TaskScheduler.dll"
  Delete "$INSTDIR\Microsoft.Win32.TaskSchedulerEditor.dll"
  Delete "$INSTDIR\TimeSpan2.Core.dll"
  Delete "$INSTDIR\TimeSpan2.dll"
  ; uninstaller, don't remove
  Delete "$INSTDIR\uninst.exe"

  ; remove install directories
  RMDir "$INSTDIR"
  RMDir "$INSTDIR\.."

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

  SetAutoClose true
SectionEnd
