import React, { useState, useEffect } from 'react';
import {
  Container,
  Typography,
  Box,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  AppBar,
  Toolbar,
  Snackbar,
  Alert
} from '@mui/material';
import { getAssetsByPlayer, registerPlayer, createAsset } from './api';

function App() {
  const [playerAssets, setPlayerAssets] = useState([]);
  const [loading, setLoading] = useState(false);
  const [playerDialogOpen, setPlayerDialogOpen] = useState(false);
  const [assetDialogOpen, setAssetDialogOpen] = useState(false);
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  const [playerForm, setPlayerForm] = useState({
    playerName: '',
    fullName: '',
    age: '',
    level: '',
    email: ''
  });

  const [assetForm, setAssetForm] = useState({
    assetName: '',
    levelRequire: ''
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await getAssetsByPlayer();
      setPlayerAssets(response.data || []);
    } catch (error) {
      showSnackbar('Error fetching data: ' + error.message, 'error');
    } finally {
      setLoading(false);
    }
  };

  const showSnackbar = (message, severity = 'success') => {
    setSnackbar({ open: true, message, severity });
  };

  const handleCloseSnackbar = () => {
    setSnackbar({ ...snackbar, open: false });
  };

  const handlePlayerSubmit = async () => {
    try {
      await registerPlayer({
        ...playerForm,
        level: parseInt(playerForm.level) || 1
      });
      showSnackbar('Player registered successfully!');
      setPlayerDialogOpen(false);
      setPlayerForm({ playerName: '', fullName: '', age: '', level: '', email: '' });
    } catch (error) {
      showSnackbar('Error: ' + (error.message || 'Failed to register player'), 'error');
    }
  };

  const handleAssetSubmit = async () => {
    try {
      await createAsset({
        ...assetForm,
        levelRequire: parseInt(assetForm.levelRequire) || 1
      });
      showSnackbar('Asset created successfully!');
      setAssetDialogOpen(false);
      setAssetForm({ assetName: '', levelRequire: '' });
    } catch (error) {
      showSnackbar('Error: ' + (error.message || 'Failed to create asset'), 'error');
    }
  };

  return (
    <Box sx={{ flexGrow: 1, minHeight: '100vh', bgcolor: '#f5f5f5' }}>
      <AppBar position="static" sx={{ bgcolor: '#1976d2' }}>
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            BattleGame - Player Asset Management
          </Typography>
        </Toolbar>
      </AppBar>

      <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
        <Box sx={{ mb: 3, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <Typography variant="h5" component="h1" sx={{ fontWeight: 'bold', color: '#333' }}>
            Player Assets Report
          </Typography>
          <Box>
            <Button
              variant="contained"
              color="primary"
              onClick={() => setPlayerDialogOpen(true)}
              sx={{ mr: 1 }}
            >
              Register Player
            </Button>
            <Button
              variant="contained"
              color="secondary"
              onClick={() => setAssetDialogOpen(true)}
            >
              Create Asset
            </Button>
          </Box>
        </Box>

        <TableContainer component={Paper} elevation={3}>
          <Table sx={{ minWidth: 650 }} aria-label="player assets table">
            <TableHead sx={{ bgcolor: '#1976d2' }}>
              <TableRow>
                <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>No</TableCell>
                <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>Player Name</TableCell>
                <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>Level</TableCell>
                <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>Age</TableCell>
                <TableCell sx={{ color: 'white', fontWeight: 'bold' }}>Asset Name</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {loading ? (
                <TableRow>
                  <TableCell colSpan={5} align="center">Loading...</TableCell>
                </TableRow>
              ) : playerAssets.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={5} align="center">No data available</TableCell>
                </TableRow>
              ) : (
                playerAssets.map((row, index) => (
                  <TableRow key={index} sx={{ '&:nth-of-type(odd)': { bgcolor: '#f9f9f9' } }}>
                    <TableCell>{row.No}</TableCell>
                    <TableCell>{row.PlayerName}</TableCell>
                    <TableCell>{row.Level}</TableCell>
                    <TableCell>{row.Age}</TableCell>
                    <TableCell>{row.AssetName}</TableCell>
                  </TableRow>
                ))
              )}
            </TableBody>
          </Table>
        </TableContainer>

        <Box sx={{ mt: 2, textAlign: 'center' }}>
          <Button variant="outlined" onClick={fetchData} disabled={loading}>
            Refresh Data
          </Button>
        </Box>
      </Container>

      {/* Register Player Dialog */}
      <Dialog open={playerDialogOpen} onClose={() => setPlayerDialogOpen(false)}>
        <DialogTitle>Register New Player</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            label="Player Name"
            fullWidth
            variant="outlined"
            value={playerForm.playerName}
            onChange={(e) => setPlayerForm({ ...playerForm, playerName: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Full Name"
            fullWidth
            variant="outlined"
            value={playerForm.fullName}
            onChange={(e) => setPlayerForm({ ...playerForm, fullName: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Age"
            fullWidth
            variant="outlined"
            value={playerForm.age}
            onChange={(e) => setPlayerForm({ ...playerForm, age: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Level"
            type="number"
            fullWidth
            variant="outlined"
            value={playerForm.level}
            onChange={(e) => setPlayerForm({ ...playerForm, level: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Email"
            type="email"
            fullWidth
            variant="outlined"
            value={playerForm.email}
            onChange={(e) => setPlayerForm({ ...playerForm, email: e.target.value })}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setPlayerDialogOpen(false)}>Cancel</Button>
          <Button onClick={handlePlayerSubmit} variant="contained" color="primary">
            Register
          </Button>
        </DialogActions>
      </Dialog>

      {/* Create Asset Dialog */}
      <Dialog open={assetDialogOpen} onClose={() => setAssetDialogOpen(false)}>
        <DialogTitle>Create New Asset</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            label="Asset Name"
            fullWidth
            variant="outlined"
            value={assetForm.assetName}
            onChange={(e) => setAssetForm({ ...assetForm, assetName: e.target.value })}
          />
          <TextField
            margin="dense"
            label="Level Require"
            type="number"
            fullWidth
            variant="outlined"
            value={assetForm.levelRequire}
            onChange={(e) => setAssetForm({ ...assetForm, levelRequire: e.target.value })}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setAssetDialogOpen(false)}>Cancel</Button>
          <Button onClick={handleAssetSubmit} variant="contained" color="secondary">
            Create
          </Button>
        </DialogActions>
      </Dialog>

      <Snackbar open={snackbar.open} autoHideDuration={6000} onClose={handleCloseSnackbar}>
        <Alert onClose={handleCloseSnackbar} severity={snackbar.severity} sx={{ width: '100%' }}>
          {snackbar.message}
        </Alert>
      </Snackbar>
    </Box>
  );
}

export default App;
