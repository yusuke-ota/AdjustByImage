#if UNITY_EDITOR
using System.Runtime.CompilerServices;

// 指定したdllから、internalアクセスレベルへのアクセスを許可する
[assembly: InternalsVisibleTo("EditMode")] //adfで定義した名前と一致させる
#endif