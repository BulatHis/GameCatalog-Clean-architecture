import * as React from 'react';
import {useEffect, useState} from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import {useLocation, useNavigate} from 'react-router-dom';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {createTheme, ThemeProvider} from '@mui/material/styles';


const defaultTheme = createTheme();

export default function MailCheck() {
    const [password, setPassword] = useState([]);
    const [errorPassword, setErrorPassword] = useState('');
    const location = useLocation();
    const email = location.state?.email;
    const text = location.state?.text;
    const [isButtonDisabled, setIsButtonDisabled] = useState(true);
    const [countdown, setCountdown] = useState(
        parseInt(localStorage.getItem('countdown'), 10) || 60
    );
    const navigate = useNavigate();
    

    useEffect(() => {
        const timer = setInterval(() => {
            setCountdown((prevCountdown) => prevCountdown - 1);
        }, 1000);

        return () => clearInterval(timer);
    }, []);

    useEffect(() => {
        if (countdown === 0) {
            setIsButtonDisabled(false);
        }
    }, [countdown]);

    useEffect(() => {
        localStorage.setItem('countdown', countdown.toString());
    }, [countdown]);

    const validatePassword = (password) => {
        if (password.length < 1 || password.length > 7) {
            setErrorPassword('Code must be 6 characters');
        } else {
            setErrorPassword('');
        }
    };
    const checkOnClick = () => {
        if (errorPassword !== '') {
            Error('Ошибка при отправке данных на сервер');
        } else {
            const url = new URL('http://localhost:4044/confirmuser');
            url.searchParams.append('email', email);
            url.searchParams.append('SeckretKey', password);
            fetch(url, {
                method: 'GET', mode: 'cors', headers: {
                    'Content-Type': 'application/json'
                }
            }).then((data) => {
                if (data.ok) {
                    const userId = data.userId;
                    localStorage.setItem('userId', userId);
                    navigate('/');
                } else {
                    throw new Error('Ошибка при отправке данных на сервер');
                }
            }).catch(error => {
                console.error('Ошибка при отправке данных на сервер', error);
            });
        }
    }

    const newCodeOnClick = () => {
        setIsButtonDisabled(true);
        setCountdown(60);

        const url = `http://localhost:4044/getmail?email=${encodeURIComponent(email)}`;
        fetch(url, {
            method: 'GET',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => {

                if (!response.ok) {
                    throw new Error('Error sending data to the server');
                }

            })
            .catch((error) => {
                console.error('Error sending data to the server', error);
            });
    };


    return (<ThemeProvider theme={defaultTheme}>
        <Container component="main" maxWidth="xs">
            <CssBaseline/>
            <Box
                sx={{
                    marginTop: 30, display: 'flex', flexDirection: 'column', alignItems: 'center',
                }}
            >
                <Typography component="h2" variant="h6">
                    {text}
                </Typography>
                <Typography component="h1" variant="h5">
                    Confirm code
                </Typography>
                <Box noValidate sx={{mt: 3}}>
                    <Grid container spacing={4} justifyContent="center">
                        <Grid item xs={10}>
                            <TextField
                                onChange={(p) => {
                                    setPassword(p.target.value);
                                }}
                                onBlur={(p) => {
                                    validatePassword(p.target.value);
                                }}
                                required
                                fullWidth
                                name="password"
                                label="Code"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                                error={Boolean(errorPassword)}
                                helperText={errorPassword}
                            />
                        </Grid>
                    </Grid>
                    <Button
                        onClick={checkOnClick}
                        fullWidth
                        variant="contained"
                        sx={{mt: 3, mb: 2}}
                    >
                        Confirm
                    </Button>
                    {!isButtonDisabled && (<Button
                        onClick={newCodeOnClick}
                        fullWidth
                        variant="outlined"
                        sx={{mt: 4, mb: 2}}
                    >
                        Send new code
                    </Button>)}
                    {isButtonDisabled && (<div>
                        <p>The new code is available in : {countdown} seconds</p>
                    </div>)}
                </Box>
            </Box>
        </Container>
    </ThemeProvider>);
}