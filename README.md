# Unity-Script
Unityのスクリプト配布所。気に入ったら「★Star」ください。

## スクリプト一覧

### H2A_2dPlayer
#### 用途
ツクール風の2D移動を再現するスクリプト。
H2A_CharaChipがADDされたオブジェクトに一緒に付けることで、
ツクール風の歩行アニメーションを実現することができる。（勿論無くても動作する。）

#### 前提
このスクリプトがADDされたオブジェクトの子供オブジェクトには、
SpriteRendererがADDされていなければならない。

### こんな感じ
<blockquote class="twitter-tweet" data-lang="ja"><p lang="ja" dir="ltr">Unityでアレックスを歩かせてみるテスト<a href="https://twitter.com/hashtag/Unity3d?src=hash">#Unity3d</a> <a href="https://twitter.com/hashtag/RPG%E3%83%84%E3%82%AF%E3%83%BC%E3%83%AB2000?src=hash">#RPGツクール2000</a> <a href="https://twitter.com/hashtag/%E3%83%84%E3%82%AF%E3%83%BC%E3%83%AB2000?src=hash">#ツクール2000</a> <a href="https://twitter.com/hashtag/rpgmaker?src=hash">#rpgmaker</a> <a href="https://t.co/IpTebKfuSA">pic.twitter.com/IpTebKfuSA</a></p>&mdash; はどはど@Had2Apps🔵 (@katai5plate) <a href="https://twitter.com/katai5plate/status/909691061596913664">2017年9月18日</a></blockquote>
<script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>

### H2A_CharaChip
#### 用途
RPGツクール2000、2003、VX、MVの歩行グラフィックをUnityで扱いやすくするスクリプト。
H2A_2dPlayerがADDされたオブジェクトに一緒に付けることで、
ツクール風の歩行アニメーションを実現することができる。

#### 前提
使用する歩行グラフィック素材はRTPの素材規格そのままの構成の画像を使用し、
MultipleSprite化して96枚の画像に分割すること。また、透過PNGにすることを推奨。