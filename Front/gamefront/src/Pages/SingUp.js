import * as React from 'react';
import {useState} from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import {useNavigate} from 'react-router-dom';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {createTheme, ThemeProvider} from '@mui/material/styles';


const defaultTheme = createTheme();


export default function SignUp() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [errorPassword, setErrorPassword] = useState('');
    const [errorName, setErrorName] = useState('');
    const [errorEmail, setErrorEmail] = useState('');

    const navigate = useNavigate();
    const validatePassword = (password) => {
        if (password.length < 5 || password.length > 15) {
            setErrorPassword('Password must be between 5 and 15 characters');
        } else {
            setErrorPassword('');
        }
    };

    const validateName = (password) => {
        if (password.length < 1 || password.length > 13) {
            setErrorName('Name must be between 1 and 13 characters');
        } else {
            setErrorName('');
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
  //  localStorage.setItem("accessToken",data.value.accessToken);
   // localStorage.setItem("refreshToken",data.value.refreshToken);
    const singUpOnClick = () => {
        if (errorPassword !== '' || errorName !== '' || errorEmail !== '') {
            Error('Ошибка при отправке данных на сервер');
        } else {
            fetch('http://localhost:4044/registration', {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email: email,
                    password: password,
                    name: name
                    
                })
            }).then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Ошибка при отправке данных на сервер');
                }
            })
                .then((data) => {
                    const url = `http://localhost:4044/getmail?email=${encodeURIComponent(email)}`;
                    fetch(url, {
                        method: 'GET',
                        mode: 'cors',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(response => {
                        if (response.ok) {
                            navigate('/MailCheck', {state: {email: email}});
                        } else {
                            throw new Error('Ошибка при отправке данных на сервер');
                        }
                    })
                        .catch(error => {
                            console.error('Ошибка при отправке данных на сервер', error);
                        });
                })
                .catch(error => {
                    console.error('Ошибка при отправке данных на сервер', error);
                });
        }
    }


    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline/>
                <Box
                    sx={{
                        marginTop: 30,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Typography component="h1" variant="h5">
                        Sign up
                    </Typography>
                    <Box noValidate sx={{mt: 3}}>
                        <Grid container spacing={4} justifyContent="center">
                            <Grid item xs={10}>
                                <TextField
                                    onChange={(n) => {
                                        setName(n.target.value)
                                    }}
                                    onBlur={(p) => {
                                        validateName(p.target.value);
                                    }}
                                    autoComplete="given-name"
                                    name="Name"
                                    required
                                    fullWidth
                                    id="Name"
                                    label="Name"
                                    error={Boolean(errorName)}
                                    helperText={errorName}
                                />
                            </Grid>
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
                                    error={Boolean(errorPassword)}
                                    helperText={errorPassword}
                                />
                            </Grid>
                        </Grid>
                        <Button
                            onClick={singUpOnClick}
                            fullWidth
                            variant="contained"
                            sx={{mt: 3, mb: 2}}
                        >
                            Sign Up
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link href="/SingIn" variant="body2">
                                    Already have an account? Sign in
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </ThemeProvider>
    );
}

