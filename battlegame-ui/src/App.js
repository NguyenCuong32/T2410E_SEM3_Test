import { useEffect, useState } from "react";

function App() {

  const API = "http://localhost:7071/api";

  const [players, setPlayers] = useState([]);
  const [playerList, setPlayerList] = useState([]);
  const [showPlayers, setShowPlayers] = useState(false);

  const [playerName, setPlayerName] = useState("");
  const [fullName, setFullName] = useState("");
  const [age, setAge] = useState("");
  const [level, setLevel] = useState("");

  const [assetName, setAssetName] = useState("");
  const [assetType, setAssetType] = useState("");

  const [createdAssetId, setCreatedAssetId] = useState(null);


  const loadData = () => {
    fetch(`${API}/getassetsbyplayer`)
      .then(res => res.json())
      .then(data => setPlayers(data));
  };

  const loadPlayers = () => {
    fetch(`${API}/getplayers`)
      .then(res => res.json())
      .then(data => setPlayerList(data));
  };

  useEffect(() => {
    loadData();
    loadPlayers();
  }, []);


  const addPlayer = async () => {

    await fetch(`${API}/registerplayer`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        playerName,
        fullName,
        age: Number(age),
        level: Number(level)
      })
    });

    alert("Player added");

    setPlayerName("");
    setFullName("");
    setAge("");
    setLevel("");

    loadPlayers();
  };


  const addAsset = async () => {

    const res = await fetch(`${API}/createasset`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        assetName,
        assetType
      })
    });

    const data = await res.json();

    setCreatedAssetId(data.assetId);

    alert("Asset created. Select a player.");

    setAssetName("");
    setAssetType("");

    setShowPlayers(true);
  };


  const linkAsset = async (playerId) => {

    await fetch(`${API}/assignasset`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        playerId,
        assetId: createdAssetId
      })
    });

    alert("Asset linked!");

    setShowPlayers(false);

    loadData();
  };


  return (

    <div style={container}>

      <h1>Battle Game Management</h1>

      {/* Add Player */}
      <div style={card}>

        <h3>Add Player</h3>

        <input
          placeholder="Player Name"
          value={playerName}
          onChange={e => setPlayerName(e.target.value)}
        />

        <input
          placeholder="Full Name"
          value={fullName}
          onChange={e => setFullName(e.target.value)}
        />

        <input
          placeholder="Age"
          value={age}
          onChange={e => setAge(e.target.value)}
        />

        <input
          placeholder="Level"
          value={level}
          onChange={e => setLevel(e.target.value)}
        />

        <button style={button} onClick={addPlayer}>Add Player</button>

      </div>


      {/* Add Asset */}
      <div style={card}>

        <h3>Add Asset</h3>

        <input
          placeholder="Asset Name"
          value={assetName}
          onChange={e => setAssetName(e.target.value)}
        />

        <input
          placeholder="Asset Type"
          value={assetType}
          onChange={e => setAssetType(e.target.value)}
        />

        <button style={button} onClick={addAsset}>Create Asset</button>

      </div>


      {/* Player selection popup */}
      {showPlayers && (

        <div style={popup}>

          <h3>Select Player</h3>

          {playerList.map(p => (

            <button
              key={p.PlayerId}
              style={playerButton}
              onClick={() => linkAsset(p.PlayerId)}
            >
              {p.PlayerName}
            </button>

          ))}

        </div>

      )}


      {/* Table */}
      <table style={tableStyle}>

        <thead>
          <tr>
            <th>No</th>
            <th>Player</th>
            <th>Level</th>
            <th>Age</th>
            <th>Asset</th>
          </tr>
        </thead>

        <tbody>

          {players.map((p, index) => (

            <tr key={index}>
              <td>{index + 1}</td>
              <td>{p.PlayerName}</td>
              <td>{p.Level}</td>
              <td>{p.Age}</td>
              <td>{p.AssetName}</td>
            </tr>

          ))}

        </tbody>

      </table>

    </div>
  );
}


const container = {
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  fontFamily: "Arial",
  padding: "40px",
  backgroundColor: "#f4f6f8",
  minHeight: "100vh"
};


const card = {
  border: "1px solid #ddd",
  padding: "20px",
  margin: "10px",
  borderRadius: "10px",
  width: "320px",
  display: "flex",
  flexDirection: "column",
  gap: "10px",
  background: "white",
  boxShadow: "0 3px 10px rgba(0,0,0,0.1)"
};


const button = {
  padding: "10px",
  border: "none",
  backgroundColor: "#2563eb",
  color: "white",
  borderRadius: "5px",
  cursor: "pointer"
};


const tableStyle = {
  borderCollapse: "collapse",
  marginTop: "40px",
  width: "70%",
  background: "white",
  boxShadow: "0 3px 10px rgba(0,0,0,0.1)"
};


const popup = {
  border: "1px solid #ddd",
  padding: "20px",
  marginTop: "20px",
  background: "white",
  borderRadius: "10px",
  boxShadow: "0 3px 10px rgba(0,0,0,0.1)"
};


const playerButton = {
  margin: "5px",
  padding: "8px 12px",
  border: "none",
  borderRadius: "5px",
  background: "#10b981",
  color: "white",
  cursor: "pointer"
};


export default App;