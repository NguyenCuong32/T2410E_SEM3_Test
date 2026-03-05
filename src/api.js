const API = "http://localhost:7143/api"

export const registerPlayer = async (player) => {
  const res = await fetch(`${API}/registerplayer`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(player)
  })
  return res.json()
}

export const createAsset = async (asset) => {
  const res = await fetch(`${API}/createasset`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(asset)
  })
  return res.json()
}

export const getAssets = async (playerId) => {
  const res = await fetch(`${API}/getassetsbyplayer?playerId=${playerId}`)
  return res.json()
}