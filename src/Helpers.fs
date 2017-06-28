module Helpers
open System
open System.Collections
open UnityEngine
open UnityEngine.AI

let moveNorth = KeyCode.W
let moveWest = KeyCode.A
let moveSouth = KeyCode.S
let moveEast = KeyCode.D
let moveDrop = KeyCode.Space
let rotCw = KeyCode.Period
let rotCcw = KeyCode.Comma

let (|KeyPressed|) (key:KeyCode) =
    Input.GetKeyDown(key)


type KeyPressMap = {key:KeyCode; action:(unit->MonoBehaviour)}

let respondToKeyPress (mappings: KeyPressMap list): unit =
    mappings
    |> List.filter (fun m -> Input.GetKeyDown(m.key) )
    |> List.iter (fun m-> m.action() |> ignore )

let stopAllMotion (item: MonoBehaviour) =
    let body = item.GetComponent<Rigidbody>()
    body.velocity <- Vector3.zero
    body.angularVelocity <- Vector3.zero
    item

let MoveTo (destPos:Vector3) rate (this: MonoBehaviour)=
    let origPos = this.transform.position
    seq{
        let mutable i = 0.0f
        while i < 1.0f do
        i <- i + Time.deltaTime * rate 
        this.transform.position <- Vector3.Lerp(origPos, destPos, i )  
        yield this.transform.position
        // stopAllMotion this |> ignore
        // yield this.transform.position
    }
    :?> IEnumerator
    |> this.StartCoroutine
    |> ignore
    this

let TurnTo (destRot:Quaternion) rate (this: MonoBehaviour)=
    let origRot = this.transform.rotation
    seq{
        let mutable i = 0.0f
        while i < 1.0f do
        i <- i + Time.deltaTime * rate 
        this.transform.rotation <- Quaternion.Lerp(origRot, destRot, i )  
        yield this.transform.rotation
        // stopAllMotion this |> ignore
        // yield this.transform.rotation
    }
    :?> IEnumerator
    |> this.StartCoroutine
    |> ignore
    this

let offset (vector:Vector3) (this: MonoBehaviour)=
    let origPos = this.transform.position
    origPos + vector


let MoveBy (vector:Vector3) rate  (this: MonoBehaviour)=
    let origPos = this.transform.position
    let destPos = origPos + vector
    MoveTo destPos rate this

let TurnBy deg (this: MonoBehaviour)=
    let orig = this.transform.rotation
    let dest = orig * Quaternion.Euler(Vector3.up * deg );
    this |> TurnTo dest 3.0f

let agentMove (this: MonoBehaviour) destination  =
    this.GetComponent<NavMeshAgent>().destination <- destination
    this

let agentAtDestination (this: MonoBehaviour) action =
    let agent = this.GetComponent<NavMeshAgent>()
    match agent.pathPending, agent.remainingDistance with
    | false, d when d < agent.radius -> 
        action this
    | _, _ -> this

