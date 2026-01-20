import React, { useEffect, useState } from 'react';
import { comicService } from '../services/comicService';
import { Book, Plus, DollarSign, Star } from 'lucide-react';
import styles from './HomePage.module.css';

const HomePage = () => {
    const [comics, setComics] = useState([]);
    const [loading, setLoading] = useState(true);

    /* Existing state */
    const [showModal, setShowModal] = useState(false);
    const [editingComic, setEditingComic] = useState(null);
    const [comicForm, setComicForm] = useState({
        title: '',
        author: '',
        pricePerDay: '',
        quantity: ''
    });

    useEffect(() => {
        fetchComics();
    }, []);

    const fetchComics = async () => {
        try {
            const data = await comicService.getAll();
            setComics(data);
        } catch (error) {
            console.error('Failed to fetch comics:', error);
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this comic?')) {
            try {
                await comicService.delete(id);
                fetchComics();
            } catch (error) {
                alert('Failed to delete comic');
            }
        }
    };

    const openModal = (comic = null) => {
        if (comic) {
            setEditingComic(comic);
            setComicForm({
                title: comic.title,
                author: comic.author,
                pricePerDay: comic.pricePerDay,
                quantity: comic.quantity
            });
        } else {
            setEditingComic(null);
            setComicForm({ title: '', author: '', pricePerDay: '', quantity: '' });
        }
        setShowModal(true);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (editingComic) {
                await comicService.update(editingComic.id || editingComic.bookId, comicForm);
            } else {
                await comicService.create(comicForm);
            }
            setShowModal(false);
            fetchComics();
        } catch (error) {
            alert('Operation failed');
        }
    };

    if (loading) return <div className="container">Loading...</div>;

    return (
        <div className="container">
            <div className={styles.header}>
                <h1>Comic Collection</h1>
                <button className="btn-primary" onClick={() => openModal()}>
                    <Plus size={18} style={{ marginRight: '8px' }} /> Add Comic
                </button>
            </div>

            <div className={styles.grid}>
                {comics.map((comic) => (
                    <div key={comic.id || comic.bookId} className={`${styles.card} glass-panel`}>
                        <div className={styles.cardHeader}>
                            <Book className={styles.bookIcon} size={32} />
                            <span className={styles.badge}>{comic.quantity > 0 ? 'In Stock' : 'Out of Stock'}</span>
                        </div>
                        <h3>{comic.title}</h3>
                        <p className={styles.author}>by {comic.author}</p>
                        <div className={styles.priceTag}>
                            <DollarSign size={16} />
                            <span>{comic.pricePerDay}/day</span>
                        </div>
                        <div className={styles.actions}>
                            <button
                                className={styles.actionBtn}
                                onClick={() => openModal(comic)}
                            >
                                Edit
                            </button>
                            <button
                                className={`${styles.actionBtn} ${styles.deleteBtn}`}
                                onClick={() => handleDelete(comic.id || comic.bookId)}
                            >
                                Delete
                            </button>
                        </div>
                    </div>
                ))}
            </div>

            {showModal && (
                <div className={styles.modalOverlay}>
                    <div className={`glass-panel ${styles.modal}`}>
                        <h2>{editingComic ? 'Edit Comic' : 'Add New Comic'}</h2>
                        <form onSubmit={handleSubmit}>
                            <div className={styles.field}>
                                <label>Title</label>
                                <input
                                    type="text"
                                    value={comicForm.title}
                                    onChange={e => setComicForm({ ...comicForm, title: e.target.value })}
                                    required
                                />
                            </div>
                            <div className={styles.field}>
                                <label>Author</label>
                                <input
                                    type="text"
                                    value={comicForm.author}
                                    onChange={e => setComicForm({ ...comicForm, author: e.target.value })}
                                    required
                                />
                            </div>
                            <div className={styles.row}>
                                <div className={styles.field}>
                                    <label>Price/Day</label>
                                    <input
                                        type="number"
                                        value={comicForm.pricePerDay}
                                        onChange={e => setComicForm({ ...comicForm, pricePerDay: e.target.value })}
                                        required
                                    />
                                </div>
                                <div className={styles.field}>
                                    <label>Quantity</label>
                                    <input
                                        type="number"
                                        value={comicForm.quantity}
                                        onChange={e => setComicForm({ ...comicForm, quantity: e.target.value })}
                                        required
                                    />
                                </div>
                            </div>
                            <div className={styles.modalActions}>
                                <button type="button" onClick={() => setShowModal(false)} className={styles.cancelBtn}>Cancel</button>
                                <button type="submit" className="btn-primary">{editingComic ? 'Update' : 'Create'}</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default HomePage;
