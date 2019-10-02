# Arcaea谱面变速器


#### 功能
**变速倍数：0.5-2.0倍（步长0.05)**
- **aff、txt Arcaea谱面变速**

	目前分为小节线/bpm变速、小节线/bpm不变速（谱面流速和原谱一致，会显得notes分散，缺点是小节线无法对准）
	
- **可选mp3、ogg音频变速**

	可选意思是在选择谱面之后弹出选择音频对话框下面路径右边

	音质在256k左右，变速延迟+-100ms以内(其实还没测试，只是这个变速库soundtouch自己说的(bushi
	
- **自动打包**

	> 注意事项：
	
	>自动打包功能生成的文件夹名、songlist 

	json名是根据**当前变速谱面路径**来确定的，请按照官铺song放置方式放置再启用自动打包
	
	在原谱面目录下自动生成打包好的文件夹
	
	文件夹名为当前谱面目录名+倍数

	①自动生成songlist Json文件，可直接复制进主songlist

	②自动按照格式改名生成 0.aff、1.aff、2.aff、base.ogg （1.aff是小节线/bpm不变速，2.aff是小节线/bpm变速）
	
	③若谱面目录含有base.jpg、base_256.jpg会自动复制进打包文件夹

- **按照官方songlist生成json**
	
	当前songlist文件版本 2.3.2c 

	请不要删除songlist文件，但可以更新它（ 
	
	也可以替换为自制共存的songlist文件进行自制谱变速（大概可以 不行滴滴我

	歌曲名、bpm_base、歌曲难度等按照songlist还原

	甚至打歌面板...但这一定要记得放进相应的打歌背景（名字对应即可）进bg文件夹，否则会引起闪退

	


## 更新历程

> v3.3

修复mp3变速BUG(一直没在用所以没发现

新增快速打包中歌曲json信息可根据自定义的songlist（默认为官方）填写


> v3.2

新增ogg音频编码支持

... ...
> v2.0.1

忘了


> v2.0

界面重构、变速开放至0.5-2.0、变速bug修复

> v1.0

谱面变速功能实现，变速0.5-0.95

## 引用/参考项目/第三方库

[SoxSharp](https://github.com/igece/SoxSharp "SoxSharp")

[NAudio](https://github.com/naudio/NAudio "NAudio")

[NAudio.Vorbis](https://github.com/naudio/Vorbis "NAudio.Vorbis")

## 吐槽

初学C# 继Hello World后的第二个C#程序

尝试美化界面完后才发现wpf做界面方便多了 后悔sl（

肯定会有很多bug⑧

有建议或者bug联系七奏QQ870838080
