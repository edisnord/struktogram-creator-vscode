module InitReact

open StruktogramViewer
open Parser
open Browser.Dom
open Feliz


let mutable root = ReactDOM.createRoot (document.getElementById "root")

let parseCode src =
    match Parser.Parsing.parseSource src with
    | Ok(res, _, _) ->
        let value =
            [ for x in res do
                  if (not <| Parser.Parsing.isEmptySequence x) then
                      yield x ]

        printfn $"{value}"
        value
    | Error (a, b) -> failwith <| a.ToString() + "\n\n" + b.ToString()

let public parseAndRender src =
    root.render <| DisplayDiagram.Diagram (parseCode src, "auto")