# 使用技術

## バックエンド
- C#（APIモード）
- ASP.NET Core
  - モダンなWeb APIフレームワーク
  - 高パフォーマンスでクロスプラットフォーム対応

## データアクセス
- Entity Framework Core
  - ORM（オブジェクト関係マッピング）ツール
  - データベース操作をオブジェクト指向で抽象化

## データベース
- SQL Server
  - リレーショナルデータベース管理システム（RDBMS）
- Docker
  - 開発環境用DBのコンテナ化

## プロジェクト管理
- .NET CLI
  - プロジェクトの作成、ビルド、実行を行うコマンドラインツール


## 使用ライブラリ
- System.ComponentModel.DataAnnotations
  - データモデルの検証とメタデータ管理
- System.Text.RegularExpressions
  - 正規表現の操作をサポート
- Microsoft.AspNetCore.Mvc
  - ASP.NET Core MVCフレームワーク
- Microsoft.EntityFrameworkCore
  - Entity Framework Coreの主要ライブラリ

バックエンドで使用経験のあるMVCモデルで構築


## 開発補助
- dotnetコマンド
  - 開発作業の効率化
  - `dotnet build`、`dotnet run` などを使用


## デプロイ
- Azure App Service
  - バックエンドアプリケーションのホスティング

## セキュリティ
- cors
  - App Serviceでリクエスト可能なURLを指定しています。
- 接続文字列
  - SQL Databaseへの接続文字列をApp Serviceの環境変数に入れてサービス内で取得して利用しています。
