###カウンターアプリ
Xamarin（C#）に慣れるために初歩的なカウンターアプリの作成<br>
主にクリックされた回数をカウントし、Labelに表示していくだけの簡単な内容<br>
Buttonなどのイベントリスナーの書き方がiOS, Androidともに普段と異なっていて戸惑った<br>
- iOS(swift)
```swift
let Button:UIButton = UIButton()
Button.addTarget(self, action: Selector("myFunc"), forControlEvents: .TouchUpInside)
Button.tag = 0

func myFunc(sender: UIButton){
  switch sender.tag{
    case 0:
      //処理
    case 1:
      //処理
    default:
      //処理
    break
  }
}
```
- Android(Java)
```java
final Button button = new Button(this);
button.setText("button");
button.setOnClickListener(new OnClickListener() {
  public void onClick(View v) {
    // クリック時の処理
  }
});
```

- Xamarin(C#)
```c#
// iOS
// ButtonはViewController.designer.csに定義済み
Button.TouchUpInside += delegate {
	var title = string.Format ("{0} clicks!", count++);
	Label.Text = title;
};

// Android
Button button = FindViewById<Button> (Resource.Id.myButton);
button.Click += delegate {
	Counter.Text = string.Format ("{0} clicks!", count++);
};

```
