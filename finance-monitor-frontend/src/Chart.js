import * as React from 'react';
import { LineChart, Line, XAxis, YAxis, Label, ResponsiveContainer, Tooltip, Legend } from 'recharts';
import dayjs from 'dayjs';

class Chart extends React.Component {

    constructor(props) {
      super(props);
    }
  
  render() {
    const data = this.props.financialItems.map(item  =>{
        let amount;                      
        if(item.operationType === "Expenses"){
            amount = - item.value;
        } 
        if(item.operationType === "Incomes"){
            amount = item.value;
        } 
        return { time: dayjs(item.occurredAt).format('YYYY/MM/DD'), amount: amount};
      });

    const groups = data.reduce((groups, item) => ({
        ...groups,
        [item.time]: [...(groups[item.time] || []), item]
      }), {});
      console.log(groups);    
    const dataPerDay = [];
    Object.keys(groups).forEach(function(key, index) {
        const amount = groups[key].reduce((accumulator, currentItem) => accumulator + currentItem.amount, 0);
        dataPerDay.push({ time: key, amount: amount});
    });
    dataPerDay.sort((a, b) => (a.time > b.time) ? 1 : -1)
  
    const balancePerDay = [];
    dataPerDay.reduce(
        (accumulator, currentItem) => {
          balancePerDay.push({ time: currentItem.time, amount: accumulator + currentItem.amount});
          return accumulator + currentItem.amount;
        }, 0)

    return (
      <React.Fragment>
        <ResponsiveContainer>
          <LineChart
            data={balancePerDay}
            margin={{
              top: 16,
              right: 16,
              bottom: 0,
              left: 24,
            }}
          >
            <XAxis
              dataKey="time"
            />
            <YAxis
            >
              <Label
                angle={270}
                position="left"
                style={{
                  textAnchor: 'middle',
                }}
              >
                Balance
              </Label>
            </YAxis>
            <Tooltip />
            <Legend />
            <Line
              isAnimationActive={false}
              type="monotone"
              dataKey="amount"
              dot={true}
            />
          </LineChart>
        </ResponsiveContainer>
      </React.Fragment>
    )
}

}

export default Chart;


