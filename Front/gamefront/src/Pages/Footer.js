import React from 'react';
import {Box, Typography} from '@mui/material';
import Link from "@mui/material/Link";

export default function Footer() {
    return (
        <Box sx={{bgcolor: 'background.paper', p: 3, mt: '2rem'}} component="footer">
            <Typography variant="body2" color="text.secondary" align="center">
                {'Copyright ©'}
                <Link color="inherit" href="https://mui.com/">
                    GameCatalog
                </Link>{' '}
                {new Date().getFullYear()}
                {'.'}
            </Typography>
        </Box>
    );
}