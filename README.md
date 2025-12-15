# Unity Test Project

このリポジトリは Unity プロジェクトを管理するための初期セットアップです。

## Unity Hubでの開き方
1. Unity Hub を起動します。
2. 「Open」をクリックし、このリポジトリのルートディレクトリを選択します。
3. 初回読み込み後に表示されるエディタバージョンの警告が出た場合は、推奨バージョンを選択して開いてください。

## 推奨Unityバージョン（仮）
- Unity 2022.3 LTS 系 (例: 2022.3.20f1)

## 動作確認手順（Playボタンまで）
1. プロジェクトを Unity Hub から開き、エディタが起動したら Assets/Scripts/ 以下にある `GameManager` を含むシーンを用意します（シーンがない場合は新規シーンを保存）。
2. メニューから「File」→「Save」を実行し、シーンを保存します（例: `Assets/Scenes/Main.unity`）。
3. 必要に応じて `GameManager` をシーン内の空の GameObject にアタッチします。
4. エディタ上部の「Play」ボタンを押し、再生が始まることを確認します。

## 使い方（インタラクション）
- プレイヤーカメラに `PlayerInteractor` をアタッチすると、画面中央からの Raycast でインタラクションを検出し、E キーで説明文を表示します。
- 調べたいオブジェクトには `Interactable` を追加し、Inspector で `Prompt Text`（表示される「調べる」などの文言）と `Description`（E キーで表示される説明文）を設定してください。
