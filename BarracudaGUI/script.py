from com.android.monkeyrunner import MonkeyRunner, MonkeyDevice
device = MonkeyRunner.waitForConnection(5,'HT22WW400912')
device.wake()
device.startActivity(component="com.android.settings/.Settings")
width = int(device.getProperty('display.width'))
height = int(device.getProperty('display.height'))
x = int(width / 2)
ybottom = height - 100
ytop = 200
device.drag((x,ytop ), (x, ybottom), 1, 10)
width = int(device.getProperty('display.width'))
height = int(device.getProperty('display.height'))
x = int(width / 2)
ybottom = height - 100
ytop = 200
device.drag((x,ytop ), (x, ybottom), 1, 10)
device.press('KEYCODE_BACK',MonkeyDevice.DOWN_AND_UP)
print 'sleeping for 1 seconds'
MonkeyRunner.sleep(1)
device.press('KEYCODE_BACK',MonkeyDevice.DOWN_AND_UP)
print 'sleeping for 1 seconds'
MonkeyRunner.sleep(1)
device.press('KEYCODE_BACK',MonkeyDevice.DOWN_AND_UP)
print 'sleeping for 1 seconds'
MonkeyRunner.sleep(1)
device.press('KEYCODE_BACK',MonkeyDevice.DOWN_AND_UP)
print 'sleeping for 1 seconds'
MonkeyRunner.sleep(1)
device.startActivity(component="com.android.settings/.Settings")
width = int(device.getProperty('display.width'))
height = int(device.getProperty('display.height'))
x = int(width / 2)
ybottom = height - 100
ytop = 200
device.drag((x,ytop ), (x, ybottom), 1, 10)
width = int(device.getProperty('display.width'))
height = int(device.getProperty('display.height'))
x = int(width / 2)
ybottom = height - 100
ytop = 200
device.drag((x,ytop ), (x, ybottom), 1, 10)
print 'sleeping for 1 seconds'
MonkeyRunner.sleep(1)
result = device.takeSnapshot()
print 'sleeping for 2 seconds'
MonkeyRunner.sleep(2)
print 'C:\T-Mobile\Development\Gibbon\GibbonGUI\ImageFiles\ImagesHTCVill\CurrentPhoneScreen.png '
result.writeToFile('C:/T-Mobile/Development/Gibbon/GibbonGUI/ImageFiles/ImagesHTCVill/CurrentPhoneScreen.png' ,'png')
