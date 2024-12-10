let apiRoute = "https:localhost:7286/Home"

export async function loginUser(username, password) {

    let user = {
        Username: username,
        Password: password
    }
    let options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user),
    }

    fetch(apiRoute + "/Login", options).then(response => {
        if (!response.ok) {
            throw Error(response.status);
        } else { return true; }
    }); 
}//POST - Sets Session data inside the controller for repeated user calls. Returns True if successfull

export async function fetchNotes(InputId) { 
    let info = {
        InputBoardId: InputId
    }
    let options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body:JSON.stringify(info)
    }

    fetch(apiRoute + '/GetNotes', options).then(response => {
        if (!response.ok) {
            throw Error(response.status);
        } else { return response.json() }
    });
} //GET - Get Notes as JSON object based on Board Id

export async function fetchBoards() {
    fetch(apiRoute + '/GetBoards').then(response => {
        if (!response.ok) {
            throw Error(response.status);
        } else { return response.json() }
    });
}//POST Boards for a specific user decided by Session data

export async function logoutUser() {
    fetch(apiRoute + "/Logout").then(response => {
        if (!response.ok) {
            throw Error(response.status);
        } else { return true; }
    })
}//POST - Clears User from session data and returns true if succsessfull.

export async function registerUser(username, password) {
    let user = {
        Username: username,
        Password: password
    }
    let options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    }
    fetch(apiRoute + "/MakeUser", options).then(response => {
        if (!response.ok) {
            throw Error(response.status);
        } else { return true }
    })
} //POST - Creates a new user in database and returns True if successfull

export async function createBoard(boardName) {
    let board = {
        name: boardName
    }
    let options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(board)
    }
    fetch(apiRoute + "MakeUser", options).then(response => {
        if (!response.ok) {
            throw Error(response.status)
        } else { return true }
    })
}//POST - Create a new Board under the current user defined in session data. Return true if succsessfull

export async function saveList(incomingNotes) { //Must format this as a List<JSNote>
    let List = {
        notes : incomingNotes
    }
    let options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(List)
    }
    fetch(apiRoute + "/SaveBoard", options).then(response => {
        if (!response.ok) {
            throw Error(response.status)
        } else {return true }
    })
} //POST - Send a list of JSNotes to Controller and hope it works i suppose. Returns true on success