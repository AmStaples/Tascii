import { loginUser, registerUser } from './fetchMethods'


function SignIn() {

    const LogInButtonClick = () => {
        loginUser(formData., Document.getElementById("password"))
    }

    const RegisterUserButtonClick = () => {
        registerUser(Document.getElementByid("username"), Document.getElementById("password"))
    }
    return (
        <div>           
            <p>SignIn.JSXj</p>
            <form onSubmit={LogInButtonClick}>
                <label>Username: </label>
                <input type="text" id="username"></input> <br></br>
                <label>Password: </label>
                <input type="text" id="password"></input> <br></br>
                <button onClick={LogInButtonClick}>Log In</button>
                <button onClick={RegisterUserButtonClick}>Register</button>
            </form>
        </div>
    );
}

export default SignIn;