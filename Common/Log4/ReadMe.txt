Common.Log类库的使用注意要点：
1.使用方法：
LogService.Logger.Debug(string.Empty);
LogService.Logger.Info(string.Empty);
LogService.Logger.Warn(string.Empty);
LogService.Logger.Error(string.Empty);
LogService.Logger.Fatal(string.Empty);

调用DefaultLogger的时候，日志文件默认写到 “C:/temp/Common/logs/”路径下

2.如果需要使用自定义的日志配置文件，在调用的工程根目录下添加配置文件，文件的编译动作更改为“Embedded Resource”。
调用ReConfigure(filename)方法，文件名为配置文件名称，默认文件名为"log4net.config".

3.调用 GetLogger(loggerName) 获取指定的Logger对象。
