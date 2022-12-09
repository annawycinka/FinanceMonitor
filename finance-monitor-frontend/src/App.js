import React from 'react';
import IncomesAndExpenses from './IncomesAndExpenses';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import CircularProgress from '@mui/material/CircularProgress';
import Chart from './Chart';
import CurrentBalance from './CurrentBalance';
import Backdrop from '@mui/material/Backdrop';

class App extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      financialItems: [],
      apiRootUrl: "http://localhost:5000"
    };
  }

  refreshFinancialItems = () => {
    Promise.all([
      fetch(`${this.state.apiRootUrl}/Incomes`).then(res => res.json()),
      fetch(`${this.state.apiRootUrl}/Expenses`).then(res => res.json()),
    ]).then(([incomesJson, expensesJson]) => 
    {
      const expenses = expensesJson.map(item => (
        {
          id: item.id,
          operationType: "Expenses",
          value: item.value,
          category: item.category,
          occurredAt: item.occurredAt
        }
      ));
      const incomes = incomesJson.map(item => (
        {
          id: item.id,
          operationType: "Incomes",
          value: item.value,
          category: item.category,
          occurredAt: item.occurredAt
        }
      ));
      const financialItems = incomes.concat(expenses);
      this.setState({
        itemsResponseReceived: true,
        financialItems: financialItems
      });
    }).catch((error) => {
      this.setState({
        itemsResponseReceived: true,
        error
      });
    });
  }

  getCategories= () => {
    Promise.all([
      fetch(`${this.state.apiRootUrl}/IncomeCategories`).then(res => res.json()),
      fetch(`${this.state.apiRootUrl}/ExpenseCategories`).then(res => res.json()),
    ]).then(([incomeCategoriesJson, expenseCategoriesJson]) => 
    {
      this.setState({
        categoriesResponseReceived: true,
        expenseCategories: expenseCategoriesJson,
        incomeCategories: incomeCategoriesJson
      });
    }).catch((error) => {
      this.setState({
        categoriesResponseReceived: true,
        error
      });
    });
  }

  componentDidMount() {
    this.refreshFinancialItems()  
    this.getCategories()
  }

  render() {
    const { error, itemsResponseReceived, categoriesResponseReceived, financialItems, incomeCategories, expenseCategories } = this.state;
    if (error) {
      return <Backdrop open={true}>{error.message}</Backdrop>;
    } else if (!itemsResponseReceived || !categoriesResponseReceived) {
      return <Backdrop open={true}><CircularProgress color="inherit" /></Backdrop>;
    } else {
      return (
          <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <MuiAppBar position="absolute">
              <Toolbar>
                <Typography
                  component="h1"
                  variant="h6"
                  color="inherit"
                  noWrap
                  sx={{ flexGrow: 1 }}
                >
                  Finanace Monitor
                </Typography>
              </Toolbar>
            </MuiAppBar>
    
            <Box
              component="main"
              sx={{
                backgroundColor: (theme) =>
                  theme.palette.mode === 'light'
                    ? theme.palette.grey[100]
                    : theme.palette.grey[900],
                flexGrow: 1,
                height: '100vh',
                overflow: 'auto',
              }}
            >
              <Toolbar />
              <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                <Grid container spacing={3}>
                  {/* Chart */}
                  <Grid item xs={12} md={8} lg={9}>
                    <Paper
                      sx={{
                        p: 2,
                        display: 'flex',
                        flexDirection: 'column',
                        height: 240,
                      }}
                    >
                      <Chart financialItems={financialItems}></Chart>
                    </Paper>
                  </Grid>
                  {/* Current Balance */}
                  <Grid item xs={12} md={4} lg={3}>
                    <Paper
                      sx={{
                        p: 2,
                        display: 'flex',
                        flexDirection: 'column',
                        height: 240,
                      }}
                    >
                      <CurrentBalance financialItems={financialItems}></CurrentBalance>                      
                    </Paper>
                  </Grid>                
                  {/* Table */}
                  <Grid item xs={12}>
                    <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                      <IncomesAndExpenses 
                          financialItems={financialItems}
                          apiRootUrl={this.state.apiRootUrl}
                          afterItemChangeFunc={this.refreshFinancialItems}
                          incomeCategories={incomeCategories}
                          expenseCategories={expenseCategories}>
                      </IncomesAndExpenses>
                    </Paper>
                  </Grid>
                </Grid>
              </Container>
            </Box>
          </Box>
      );
    }
  }
}

export default App;
