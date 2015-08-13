module FSharp.Macro.UseCases.Safe

open System.IO

(* Proposed syntax

type SafeFile = Safe<System.IO.File>

*)

// Generated code

type SafeFile() =
    static let attempt f = try Choice1Of2 (f()) with e -> Choice2Of2 e
    static member Open (path, mode, access) = attempt (fun _ -> File.Open (path, mode, access))
    static member ReadAllLines (path, encoding) = attempt (fun _ -> File.ReadAllLines (path, encoding))
    // and so on

// helpers
let safe = id
type SafeFileStream = FileStream

// Usage

// Make all static members safe
let _: Choice<FileStream, exn> = SafeFile.Open (@"foo.txt", FileMode.Create, FileAccess.Read)
// Make all instance members safe
let _: Choice<SafeFileStream, exn> = safe (SafeFile.Open (@"foo.txt", FileMode.Create, FileAccess.Read))
