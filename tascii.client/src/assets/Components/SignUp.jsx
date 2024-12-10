import { loginUser, registerUser } from './fetchMethods'



function SignUp() {

    return (
        <div>
            <p>
                <form>
                <p>SignUp.jsx</p>
                    <input type="text" id="username"></input>
                    <input type="text" id="password"></input>
                    <button onClick={loginUser("username", "password")}>Log In</button>
                    <button onClick={registerUser("username", "password")}>Register</button>
                </form>
            </p>
        </div>
    );
}

export default SignUp;