###ImageDownloader
「HttpClient」と「async / await」構文を用いてネットから非同期でデータをダウンロードする<br>
asyncで定義した関数をawaitで呼び出すことで非同期処理になる<br>
非同期でさせたい関数をasyncで定義しておく<br>

```c#
async Task<byte[]> Download (string Url)
{
  var httpClient = new HttpClient ();
	return await httpClient.GetByteArrayAsync (Url);
}
```

Buttonなどのイベントで呼び出す際、以下の構文ではawaitを呼び出すことができません
```c#
// iOS
Button.TouchUpInside += delegate {
  await Download(); // Error...
  ...
}

// Android
Button.Click += delegate {
  await Download(); // Error..
  ...
}
```
awaitを呼び出すにはdelegate {} を async(sender, e) => {}に書き換えます<br>
```c#
// iOS
Button.TouchUpInside += async(sender, e) => {
  await Download(); // OK!
  ...
}

// Android
Button.Click += async(sender, e) => {
  await Download(); // OK!
  ...
}
```
