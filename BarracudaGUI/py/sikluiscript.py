import subprocess
import sys

def RunSikuliScript(sikuliscriptname):
 subprocess.Popen('"C:\Program Files\Sikuli\Sikuli-ide.exe" -r ' + sikuliscriptname, shell=True)

if __name__ == '__main__':
  RunSikuliScript(sys.argv[1])