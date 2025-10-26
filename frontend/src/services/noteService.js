import axios from 'axios';

const API_URL = 'http://localhost:8080/api/note';

export const getAllNotes = (userId) => {
    return axios.get(API_URL, { params: { userId } });
};

export const getNoteById = (id) => {
    return axios.get(`${API_URL}/${id}`);
};

export const createNote = (title, content, userId) => {
    return axios.post(API_URL, { title, content, userId });
};

export const updateNote = (id, title, content) => {
    return axios.put(`${API_URL}/${id}`, { title, content });
};

export const deleteNote = (id) => {
    return axios.delete(`${API_URL}/${id}`);
};

export const pinNote = (id) => axios.put(`${API_URL}/${id}/pin`);
export const unpinNote = (id) => axios.put(`${API_URL}/${id}/unpin`);
export const archiveNote = (id) => axios.put(`${API_URL}/${id}/archive`);
export const restoreNote = (id) => axios.put(`${API_URL}/${id}/restore`);
export const changeColor = (id, color) => axios.put(`${API_URL}/${id}/color`, { color });
export const setPosition = (id, x, y) => axios.put(`${API_URL}/${id}/position`, { x, y });
export const addTag = (id, tag) => axios.put(`${API_URL}/${id}/tags/add`, { tag });
export const removeTag = (id, tag) => axios.put(`${API_URL}/${id}/tags/remove`, { tag });