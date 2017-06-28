namespace Gardener

open System
open System.Collections
open UnityEngine
open Helpers

type CameraController() =
    inherit MonoBehaviour()

    member this.Start() =
        ignore()

    member this.Update() =
        [
            {key=moveNorth; action= fun()-> this |> MoveBy  this.transform.forward 3.0f }
            {key=moveSouth; action= fun()-> this |> MoveBy (this.transform.forward * -1.0f ) 3.0f }
            {key=moveWest; action= fun()-> this |> TurnBy -90.0f }
            {key=moveEast; action= fun()-> this |> TurnBy 90.0f   }
        ]
        |>
        Helpers.respondToKeyPress

