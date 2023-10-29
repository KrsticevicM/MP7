class ReactTest extends React.Component {
    render() {
        return (
            <div className="comment">Hello World!</div>
        );
    }
}

ReactDOM.render(<ReactTest />, document.getElementById('root'));