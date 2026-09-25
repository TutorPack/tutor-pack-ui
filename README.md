# TutorPack.UI

Публичная Razor Class Library с общими Material Design-компонентами и статическими
ресурсами Tutor Pack. Пакет предназначен для Blazor-приложений на .NET 10.

## Установка

```sh
dotnet add package TutorPack.UI --version 1.0.0
```

Добавьте namespace компонентов:

```razor
@using TutorPack.UI
```

Подключите общие стили и browser helpers:

```html
<link rel="stylesheet" href="_content/TutorPack.UI/controls.css" />
<script src="_content/TutorPack.UI/controls.js"></script>
```

В пакет входят `ActionMenu`, `AppIcon`, `MaterialDialog`, `MaterialInput`,
`MaterialSearch`, `MaterialSelect`, `MaterialSwitch` и `MaterialTabs`. Scoped CSS
и ресурсы из `wwwroot` доставляются стандартным механизмом static web assets.

Основной код продукта находится в репозитории
[`TutorPack/tutor-pack`](https://github.com/TutorPack/tutor-pack), документация —
в [`TutorPack/docs`](https://github.com/TutorPack/docs).

## Сборка

```sh
dotnet restore TutorPack.UI.slnx
dotnet build TutorPack.UI.slnx --configuration Release --no-restore
dotnet pack src/TutorPack.UI/TutorPack.UI.csproj --configuration Release --no-build --output artifacts
```

Новая версия публикуется на NuGet.org после изменения `Version` и создания тега
`v<версия>`. Публикация использует GitHub OIDC Trusted Publishing и не хранит
долговременный NuGet API key.
