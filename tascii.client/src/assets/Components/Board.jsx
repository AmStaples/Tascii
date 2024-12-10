import { fetchNotes, saveList } from './fetchMethods'
import { useEffect } from 'react';



function Board() {

    const notesContainer = document.getElementById('notesContainer');

    let notes = []; // Array to store notes
    // DBID JSID Content, BoardId, xCoord, yCoord, 
    function loadNote() {
        const noteList = JSON.parse(fetchNotes());
        noteList.forEach(noteData => {addNote(noteData) })
    };

    useEffect(loadNote(), []);

    // Function to add a new note
    function addNote() {
        const note = document.createElement('textarea');
        note.classList.add('note');
        note.textContent = "New Note";

        // Make the note draggable
        makeDraggable(note);

        notesContainer.appendChild(note);
        notes.push({ content: note.value, x: note.offsetLeft, y: note.offsetTop });
        saveNotes();
    }

    // Function to make an element draggable
    function makeDraggable(element) {
        let isDragging = false;
        let startX, startY;

        element.addEventListener('mousedown', (e) => {
            isDragging = true;
            startX = e.clientX - element.offsetLeft;
            startY = e.clientY - element.offsetTop;
        });

        element.addEventListener('mousemove', (e) => {
            if (!isDragging) return;

            e.preventDefault();
            const x = e.clientX - startX;
            const y = e.clientY - startY;
            element.style.left = x + 'px';
            element.style.top = y + 'px';

            // Update note position in array
            updateNotePosition(element, x, y);
        });

        element.addEventListener('mouseup', () => {
            isDragging = false;
            saveNotes();
        });
    }

    // Function to update note position in array
    function updateNotePosition(element, x, y) {
        const index = Array.from(notesContainer.children).indexOf(element);
        if (index !== -1) {
            notes[index].x = x;
            notes[index].y = y;
        }
    }

    // Function to save notes to local storage
    function saveNotes() {
        saveList(notes);
        //Alert to say data was saved
    }

    function deleteNote() {

    }

    
    // Function to load notes from local storage
    /*
    function loadNotes() {
        const storedNotes = JSON.parse(localStorage.getItem('notes'));
        if (storedNotes) {
            storedNotes.forEach(noteData => {
                addNote();
                const note = notesContainer.lastChild;
                note.value = noteData.content;
                note.style.left = noteData.x + 'px';
                note.style.top = noteData.y + 'px';
            });
        }
    }
    */
    // Event listeners
    //useEffect(() => {
    //    addNoteButton.addEventListener('click', addNote)});
    
    //addNoteButton.addEventListener('click', addNote);
    //window.addEventListener('load', loadNotes);


    return (
        <div>
            <p>
                Notes Container
            </p>
            <div id={notesContainer} style="width:500px;height:500px"></div>
            <button onClick={addNote}>Add Note</button>
            <button onClick={saveNotes}></button>
            <button onClick={deleteNote}>Delete Note</button>
        </div>
    );
}

export default Board;