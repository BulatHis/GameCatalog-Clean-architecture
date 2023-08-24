import React from 'react';
import { Link } from 'react-router-dom';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';

export default function Header() {
    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    <Link to="/" style={{ textDecoration: 'none', color: 'inherit' }}>
                        GameCatalog
                    </Link>
                </Typography>
                <Button component={Link} to="/SingIn" color="inherit">Login</Button>
                <Button component={Link} to="/SingUp" color="inherit">Registration</Button>
            </Toolbar>
        </AppBar>
    );
}