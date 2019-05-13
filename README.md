# GoFund
This winform app is used to track the funds you're interested in. 
本工具用于辅助基金定投，即一键跟踪你所感兴趣的基金，并非帮你挑选基金：）

原理：从天天基金网上实时爬取所选基金的当日估算涨幅，以及历史排位

好处：在每日3点前了解所投基金走势，从而严格遵循涨时卖，跌时买的规则

使用方法：
1. 进入\bin\Release目录，下载所有文件到电脑上。
2. 打开allfunds.txt编辑你要跟踪的基金名单，每只基金一行，格式为：基金代码，基金名称，是否持有（yes or no）
3. 运行GoFund.exe， 点刷新 （默认每10分钟自动刷新）

![avata](/images/GoFundScreenshot.jpg)
