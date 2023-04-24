module Main

open Fable.Core.JsInterop
open Fable.Core
open Fable.Import.VSCode.Vscode
open Parser
open StruktogramViewer

[<Import("ReactPanel", from = "../build/ReactPanel")>]
let reactPanel: obj = jsNative

let openPreview extPath _ =
    window.showInformationMessage ("Do hapej preview (teorikisht)") |> ignore
    reactPanel?createOrShow extPath
    reactPanel?currentPanel?doVisualize (window.activeTextEditor.Value.document.getText())
    
    workspace.onDidSaveTextDocument.Invoke(fun (a: TextDocument) ->
        (if a.languageId.Equals "StruktoScript" && a.uri.scheme.Equals "file" then
             printfn $"{a.getText()}"
             reactPanel?currentPanel?doVisualize (a.getText()))
        None)
    |> ignore

    None

let activate (context: ExtensionContext) =
    window.showInformationMessage ("U aktivizua") |> ignore


    !! commands.registerCommand("struktogram.openPreview", (openPreview context.extensionPath))
    |> context.subscriptions.Add
