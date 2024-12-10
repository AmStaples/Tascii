import SignIn from './assets/Components/SignIn';
import SignUp from './assets/Components/SignUp'
import {BrowserRouter, Routes, Route } from "react-router-dom"
import Board from './assets/Components/Board';
import BoardList from './assets/Components/BoardList';


function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route index element={<SignIn />} />
                <Route path="/Board" element={<Board />}/>
                <Route path="/BoardList" element={<BoardList />} />
                <Route path="/SignUp" element={<SignUp />}/>
            </Routes>
        </BrowserRouter>
    )
}

export default App;