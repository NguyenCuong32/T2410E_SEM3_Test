import { useState } from "react"
import { registerPlayer, createAsset, getAssets } from "./api"

function App() {

  const [playerName, setPlayerName] = useState("")
  const [email, setEmail] = useState("")
  const [playerId, setPlayerId] = useState("")
  const [assetName, setAssetName] = useState("")
  const [assets, setAssets] = useState([])

  const handleRegister = async () => {
    const data = await registerPlayer({
      playerName,
      email
    })
    alert("Player Created")
    console.log(data)
  }

  const handleCreateAsset = async () => {
    await createAsset({
      playerId,
      assetName
    })
    alert("Asset Created")
  }

  const handleGetAssets = async () => {
    const data = await getAssets(playerId)
    setAssets(data)
  }

  return (
    <div style={{padding:"40px", fontFamily:"Arial"}}>

      <h1>Battle Game System</h1>

      <h2>Register Player</h2>

      <input
        placeholder="Player Name"
        value={playerName}
        onChange={(e)=>setPlayerName(e.target.value)}
      />

      <br/><br/>

      <input
        placeholder="Email"
        value={email}
        onChange={(e)=>setEmail(e.target.value)}
      />

      <br/><br/>

      <button onClick={handleRegister}>
        Register Player
      </button>


      <hr/>

      <h2>Create Asset</h2>

      <input
        placeholder="Player Id"
        value={playerId}
        onChange={(e)=>setPlayerId(e.target.value)}
      />

      <br/><br/>

      <input
        placeholder="Asset Name"
        value={assetName}
        onChange={(e)=>setAssetName(e.target.value)}
      />

      <br/><br/>

      <button onClick={handleCreateAsset}>
        Create Asset
      </button>


      <hr/>

      <h2>Get Assets By Player</h2>

      <button onClick={handleGetAssets}>
        Load Assets
      </button>

      <ul>
        {assets.map((a,i)=>(
          <li key={i}>
            {a.assetName}
          </li>
        ))}
      </ul>

    </div>
  )
}

export default App