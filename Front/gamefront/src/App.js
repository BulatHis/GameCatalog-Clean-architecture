import SingIn from './Pages/SingIn'
import SingUp from './Pages/SingUp'
import Header from './Pages/Header'
import Games from './Pages/Games'
import Footer from './Pages/Footer'
import MailCheck from './Pages/MailCheck'
import GamePage from './Pages/GamePage'
import WriteReview from './Pages/WriteReview'
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';

function App() {
    return (
        <div>
            <Router>
                <Header/>
                <Routes>
                    <Route path="/SingIn" element={<SingIn/>}/>
                    <Route path="/" element={<Games/>}/>
                    <Route path="/SingUp" element={<SingUp/>}/>
                    <Route path="/MailCheck" element={<MailCheck/>}/>
                    <Route path="/WriteReview/:id" element={<WriteReview/>}/>
                    <Route path="/game/:title" element={<GamePage/>}/>
                </Routes>
                <Footer/>
            </Router>
        </div>
    );
}

export default App;
