using Irony.Parsing;
using Wizard.RuleEngine.Nodes;

namespace Wizard.RuleEngine
{
    public class WizardRulesGrammar : Irony.Interpreter.InterpretedLanguageGrammar
    {
        public WizardRulesGrammar()
            : base(false)
        {
            var number = new NumberLiteral("number");
            var identifier = new IdentifierTerminal("identifier");

            var tag = new NonTerminal("tag", typeof (TagAst));
// ReSharper disable InconsistentNaming
            var wizard_rules = new NonTerminal("wizard_rules", typeof (WizardRulesAst));
            var statement = new NonTerminal("statement");
            var expression = new NonTerminal("expression");
            var binary_operators = new NonTerminal("binary_operators");
            var binary_expression = new NonTerminal("binary_expression", typeof (CSharpAstNode));

            var if_statement = new NonTerminal("if_statement", typeof (IfStatementAst));
            var else_clause = new NonTerminal("else_clause", typeof (ElseClauseAst));
            var assignment_statment = new NonTerminal("assignment_statment", typeof (CSharpAstNode));
            var premuim_statment = new NonTerminal("premuim_statment", typeof (PremuimStatmentAst));
            var quote_decision_statment = new NonTerminal("quote_decision_statment", typeof (QuoteDecisionStatmentAst));
            var quote_decisions = new NonTerminal("quote_decision", typeof (QuoteDecisionAst));
// ReSharper restore InconsistentNaming

            Root = wizard_rules;

            tag.Rule = ToTerm("@") + identifier;

            wizard_rules.Rule = MakePlusRule(wizard_rules, null, statement);

            statement.Rule = assignment_statment | if_statement;

            expression.Rule = number | tag | binary_expression;

            binary_expression.Rule = expression + binary_operators + expression;

            assignment_statment.Rule = premuim_statment | quote_decision_statment;

            premuim_statment.Rule = ToTerm("premium") + "=" + expression;

            quote_decision_statment.Rule = ToTerm("decision") + "=" + quote_decisions;
            quote_decisions.Rule = ToTerm("Accept") | "Refer" | "Decline" | "NotRequired";

            if_statement.Rule = "if" + expression + "then" + statement + else_clause + "endif";
            else_clause.Rule = Empty | "else" + statement;

            binary_operators.Rule = ToTerm("+") | "-" | "*" | "/" | "and" | "or" | "<=" | ">=" | "<" | ">" | "<>";
            RegisterOperators(50, "*", "/");
            RegisterOperators(40, "+", "-");
            RegisterOperators(30, "=", "<=", ">=", "<", ">", "<>");
            RegisterOperators(20, "and", "or");

            MarkTransient(expression, statement, assignment_statment, binary_operators);

            LanguageFlags = LanguageFlags.CreateAst;
        }
    }
}