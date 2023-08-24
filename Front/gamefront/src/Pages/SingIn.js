import * as React from 'react';
import {useEffect, useState} from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {createTheme, ThemeProvider} from '@mui/material/styles';
import {useNavigate} from "react-router-dom";


const defaultTheme = createTheme();

export default function SignIn() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [errorEmail, setErrorEmail] = useState('');
    const [text, setText] = useState('');
    const [isConfirmed, setIsConfirmed] = useState('');
    const navigate = useNavigate();
    const validatePassword = (password) => {
        if (password.length < 5 || password.length > 15) {
            setError('Password must be between 5 and 15 characters');
        } else {
            setError('');
        }
    };

    const validateEmail = (email) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            setErrorEmail('Invalid email format');
        } else {
            setErrorEmail('');
        }
    };

    const loginOnClick = () => {
        if (error !== '' || errorEmail !== '') {
            setError('Ошибка при отправке данных на сервер')
        } else {
            const url = new URL('http://localhost:4044/login');
            url.searchParams.append('email', email);
            url.searchParams.append('password', password);
            fetch(url, {
                method: 'GET', mode: 'cors', headers: {
                    'Content-Type': 'application/json'
                }
            }).then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Ошибка при отправке данных на сервер');
                }
            }).then((data) => {
                setIsConfirmed(data.isConfirmed);
                localStorage.setItem('refreshToken', data.refreshToken);
                localStorage.setItem('accessToken', data.accessToken);
                navigate('/');
            }).catch(error => {
                console.error('Ошибка при отправке данных на сервер', error);
            });
        }
    }
    useEffect(() => {
        if (isConfirmed === false) {
            const updatedText = 'Your email isn`t confirm';
            setText(updatedText);
            const url = `http://localhost:4044/getmail?email=${encodeURIComponent(email)}`;
            fetch(url, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            navigate('/MailCheck', {state: {email: email, text: updatedText}});
        }
    }, [isConfirmed, navigate]);


    return (<ThemeProvider theme={defaultTheme}>
        <Container component="main" maxWidth="xs">
            <CssBaseline/>
            <Box
                sx={{
                    marginTop: 30, display: 'flex', flexDirection: 'column', alignItems: 'center',
                }}
            >
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <Box noValidate sx={{mt: 2}}>
                    <Grid container spacing={4} justifyContent="center">
                        <Grid item xs={10}>
                            <TextField
                                onChange={(e) => {
                                    setEmail(e.target.value)
                                }}
                                onBlur={(p) => {
                                    validateEmail(p.target.value);
                                }}
                                required
                                fullWidth
                                id="email"
                                label="Email Address"
                                name="email"
                                autoComplete="email"
                                error={Boolean(errorEmail)}
                                helperText={errorEmail}
                            />
                        </Grid>
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
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                                error={Boolean(error)}
                                helperText={error}
                            />
                        </Grid>
                    </Grid>
                    <Button onClick={loginOnClick}
                            fullWidth
                            variant="contained"
                            sx={{mt: 3, mb: 2}}
                    >
                        Sign In
                    </Button>
                    <Grid container justifyContent="flex-end">
                        <Grid item>
                            <Link href="/SingUp" variant="body2">
                                Don't have an account? Sign Up
                            </Link>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
        </Container>
    </ThemeProvider>);
};