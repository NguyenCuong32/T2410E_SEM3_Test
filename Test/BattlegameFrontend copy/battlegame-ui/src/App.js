import { useEffect, useState } from "react";

export default function App() {

  const [data,setData] = useState([]);
  const [filtered,setFiltered] = useState([]);
  const [search,setSearch] = useState("");
  const [loading,setLoading] = useState(true);
  const [error,setError] = useState("");

  const loadData = () =>{
    setLoading(true);

    fetch("/api/getassetsbyplayer")
      .then(res=>res.json())
      .then(res=>{
        setData(res);
        setFiltered(res);
      })
      .catch(err=>{
        setError("Cannot connect to API");
      })
      .finally(()=>{
        setLoading(false);
      });
  }

  useEffect(()=>{
    loadData();
  },[]);

  const handleSearch = (e)=>{
    const value = e.target.value;
    setSearch(value);

    const result = data.filter(x =>
      x.playerName.toLowerCase().includes(value.toLowerCase())
    );

    setFiltered(result);
  }

  return (

    <div style={styles.container}>

      <h1 style={styles.title}>Player Assets Report</h1>

      <div style={styles.toolbar}>

        <input
          type="text"
          placeholder="Search player..."
          value={search}
          onChange={handleSearch}
          style={styles.search}
        />

        <button onClick={loadData} style={styles.button}>
          Refresh
        </button>

      </div>

      {loading && <p>Loading...</p>}

      {error && <p style={{color:"red"}}>{error}</p>}

      {!loading &&

      <table style={styles.table}>

        <thead style={styles.header}>
          <tr>
            <th>No</th>
            <th>Player</th>
            <th>Age</th>
            <th>Level</th>
            <th>Asset</th>
          </tr>
        </thead>

        <tbody>

          {filtered.map((p,i)=>(
            <tr key={i} style={styles.row}>
              <td>{i+1}</td>
              <td>{p.playerName}</td>
              <td>{p.age}</td>
              <td>{p.level}</td>
              <td>{p.assetName}</td>
            </tr>
          ))}

        </tbody>

      </table>

      }

    </div>
  );
}

const styles = {

  container:{
    padding:"40px",
    fontFamily:"Arial",
    maxWidth:"900px",
    margin:"auto"
  },

  title:{
    marginBottom:"20px"
  },

  toolbar:{
    display:"flex",
    gap:"10px",
    marginBottom:"20px"
  },

  search:{
    padding:"8px",
    width:"200px",
    border:"1px solid #ccc",
    borderRadius:"4px"
  },

  button:{
    padding:"8px 16px",
    background:"#007bff",
    color:"#fff",
    border:"none",
    borderRadius:"4px",
    cursor:"pointer"
  },

  table:{
    width:"100%",
    borderCollapse:"collapse",
    boxShadow:"0 0 5px rgba(0,0,0,0.2)"
  },

  header:{
    background:"#007bff",
    color:"#fff"
  },

  row:{
    textAlign:"center",
    borderBottom:"1px solid #ddd"
  }

}