
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF           =  0, // (EOF)
        SYMBOL_ERROR         =  1, // (Error)
        SYMBOL_WHITESPACE    =  2, // Whitespace
        SYMBOL_MINUS         =  3, // '-'
        SYMBOL_MINUSMINUS    =  4, // '--'
        SYMBOL_EXCLAMEQ      =  5, // '!='
        SYMBOL_PERCENT       =  6, // '%'
        SYMBOL_LPAREN        =  7, // '('
        SYMBOL_RPAREN        =  8, // ')'
        SYMBOL_TIMES         =  9, // '*'
        SYMBOL_DIV           = 10, // '/'
        SYMBOL_COLON         = 11, // ':'
        SYMBOL_SEMISTEPEQ    = 12, // '; step = '
        SYMBOL_LBRACE        = 13, // '{'
        SYMBOL_RBRACE        = 14, // '}'
        SYMBOL_PLUS          = 15, // '+'
        SYMBOL_PLUSPLUS      = 16, // '++'
        SYMBOL_PLUSEQ        = 17, // '+='
        SYMBOL_LT            = 18, // '<'
        SYMBOL_LTEQ          = 19, // '<='
        SYMBOL_EQ            = 20, // '='
        SYMBOL_MINUSEQ       = 21, // '-='
        SYMBOL_EQEQ          = 22, // '=='
        SYMBOL_GT            = 23, // '>'
        SYMBOL_GTEQ          = 24, // '>='
        SYMBOL_CASE          = 25, // case
        SYMBOL_DEFAULTCOLON  = 26, // 'default:'
        SYMBOL_DO            = 27, // do
        SYMBOL_ELSE          = 28, // else
        SYMBOL_FOR           = 29, // for
        SYMBOL_IF            = 30, // if
        SYMBOL_NUM           = 31, // num
        SYMBOL_STOP          = 32, // stop
        SYMBOL_SWITCH        = 33, // switch
        SYMBOL_TILL          = 34, // till
        SYMBOL_VAR           = 35, // var
        SYMBOL_WHILE         = 36, // while
        SYMBOL_ASSIGN        = 37, // <assign>
        SYMBOL_CASE2         = 38, // <case>
        SYMBOL_COMPOUND_STMT = 39, // <compound_stmt>
        SYMBOL_COND          = 40, // <cond>
        SYMBOL_DEFAULT       = 41, // <default>
        SYMBOL_DO_WHILE      = 42, // <do_while>
        SYMBOL_EXPR          = 43, // <expr>
        SYMBOL_FACTOR        = 44, // <factor>
        SYMBOL_FOR_LOOP      = 45, // <for_loop>
        SYMBOL_IF2           = 46, // <if>
        SYMBOL_MAIN          = 47, // <main>
        SYMBOL_OP            = 48, // <op>
        SYMBOL_STATEMENT     = 49, // <statement>
        SYMBOL_STEP          = 50, // <step>
        SYMBOL_STMT_LIST     = 51, // <stmt_list>
        SYMBOL_SWITCH2       = 52, // <switch>
        SYMBOL_TERM          = 53, // <term>
        SYMBOL_WHILE_LOOP    = 54  // <while_loop>
    };

    enum RuleConstants : int
    {
        RULE_MAIN                                       =  0, // <main> ::= <stmt_list>
        RULE_STMT_LIST                                  =  1, // <stmt_list> ::= <statement>
        RULE_STMT_LIST2                                 =  2, // <stmt_list> ::= <statement> <stmt_list>
        RULE_STATEMENT                                  =  3, // <statement> ::= <assign>
        RULE_STATEMENT2                                 =  4, // <statement> ::= <if>
        RULE_STATEMENT3                                 =  5, // <statement> ::= <switch>
        RULE_STATEMENT4                                 =  6, // <statement> ::= <for_loop>
        RULE_STATEMENT5                                 =  7, // <statement> ::= <while_loop>
        RULE_STATEMENT6                                 =  8, // <statement> ::= <do_while>
        RULE_STATEMENT7                                 =  9, // <statement> ::= <compound_stmt>
        RULE_COMPOUND_STMT_LBRACE_RBRACE                = 10, // <compound_stmt> ::= '{' <stmt_list> '}'
        RULE_ASSIGN_VAR_EQ                              = 11, // <assign> ::= var '=' <expr>
        RULE_EXPR_PLUS                                  = 12, // <expr> ::= <term> '+' <expr>
        RULE_EXPR_MINUS                                 = 13, // <expr> ::= <term> '-' <expr>
        RULE_EXPR                                       = 14, // <expr> ::= <term>
        RULE_TERM_TIMES                                 = 15, // <term> ::= <factor> '*' <factor>
        RULE_TERM_DIV                                   = 16, // <term> ::= <factor> '/' <factor>
        RULE_TERM_PERCENT                               = 17, // <term> ::= <factor> '%' <factor>
        RULE_TERM                                       = 18, // <term> ::= <factor>
        RULE_FACTOR_VAR                                 = 19, // <factor> ::= var
        RULE_FACTOR_NUM                                 = 20, // <factor> ::= num
        RULE_FACTOR_LPAREN_RPAREN                       = 21, // <factor> ::= '(' <expr> ')'
        RULE_IF_IF_LPAREN_RPAREN_COLON                  = 22, // <if> ::= if '(' <cond> ')' ':' <compound_stmt>
        RULE_IF_IF_LPAREN_RPAREN_COLON_ELSE             = 23, // <if> ::= if '(' <cond> ')' ':' <compound_stmt> else <compound_stmt>
        RULE_COND                                       = 24, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                      = 25, // <op> ::= '<'
        RULE_OP_GT                                      = 26, // <op> ::= '>'
        RULE_OP_LTEQ                                    = 27, // <op> ::= '<='
        RULE_OP_GTEQ                                    = 28, // <op> ::= '>='
        RULE_OP_EQEQ                                    = 29, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                = 30, // <op> ::= '!='
        RULE_SWITCH_SWITCH_LPAREN_RPAREN_COLON          = 31, // <switch> ::= switch '(' <expr> ')' ':' <case>
        RULE_CASE_CASE_COLON                            = 32, // <case> ::= case <expr> ':' <stmt_list> <case>
        RULE_CASE                                       = 33, // <case> ::= <default>
        RULE_CASE_STOP                                  = 34, // <case> ::= stop
        RULE_DEFAULT_DEFAULTCOLON                       = 35, // <default> ::= 'default:' <stmt_list>
        RULE_FOR_LOOP_FOR_LPAREN_TILL_SEMISTEPEQ_RPAREN = 36, // <for_loop> ::= for '(' <assign> till <assign> '; step = ' <step> ')' <compound_stmt>
        RULE_STEP_MINUSMINUS_VAR                        = 37, // <step> ::= '--' var
        RULE_STEP_VAR_MINUSMINUS                        = 38, // <step> ::= var '--'
        RULE_STEP_PLUSPLUS_VAR                          = 39, // <step> ::= '++' var
        RULE_STEP_VAR_PLUSPLUS                          = 40, // <step> ::= var '++'
        RULE_STEP_VAR_PLUSEQ_NUM                        = 41, // <step> ::= var '+=' num
        RULE_STEP_VAR_MINUSEQ_NUM                       = 42, // <step> ::= var '-=' num
        RULE_WHILE_LOOP_WHILE_LPAREN_RPAREN             = 43, // <while_loop> ::= while '(' <cond> ')' <compound_stmt>
        RULE_DO_WHILE_DO_WHILE_LPAREN_RPAREN            = 44  // <do_while> ::= do <compound_stmt> while '(' <cond> ')'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox errorViewer;
        ListBox tokenViewer;
        public MyParser(string filename, ListBox error, ListBox tokens)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);

            errorViewer = error;
            tokenViewer = tokens;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }


        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMISTEPEQ :
                //'; step = '
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSEQ :
                //'+='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSEQ :
                //'-='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULTCOLON :
                //'default:'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUM :
                //num
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STOP :
                //stop
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TILL :
                //till
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VAR :
                //var
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPOUND_STMT :
                //<compound_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //<default>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO_WHILE :
                //<do_while>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_LOOP :
                //<for_loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF2 :
                //<if>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MAIN :
                //<main>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH2 :
                //<switch>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_LOOP :
                //<while_loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_MAIN :
                //<main> ::= <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <statement> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<statement> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<statement> ::= <if>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<statement> ::= <switch>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT4 :
                //<statement> ::= <for_loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT5 :
                //<statement> ::= <while_loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT6 :
                //<statement> ::= <do_while>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT7 :
                //<statement> ::= <compound_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPOUND_STMT_LBRACE_RBRACE :
                //<compound_stmt> ::= '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_VAR_EQ :
                //<assign> ::= var '=' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <term> '+' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <term> '-' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <factor> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <factor> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <factor> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_VAR :
                //<factor> ::= var
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_NUM :
                //<factor> ::= num
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN :
                //<factor> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_COLON :
                //<if> ::= if '(' <cond> ')' ':' <compound_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_IF_LPAREN_RPAREN_COLON_ELSE :
                //<if> ::= if '(' <cond> ')' ':' <compound_stmt> else <compound_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_SWITCH_LPAREN_RPAREN_COLON :
                //<switch> ::= switch '(' <expr> ')' ':' <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_COLON :
                //<case> ::= case <expr> ':' <stmt_list> <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE :
                //<case> ::= <default>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_STOP :
                //<case> ::= stop
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT_DEFAULTCOLON :
                //<default> ::= 'default:' <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_LOOP_FOR_LPAREN_TILL_SEMISTEPEQ_RPAREN :
                //<for_loop> ::= for '(' <assign> till <assign> '; step = ' <step> ')' <compound_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS_VAR :
                //<step> ::= '--' var
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_VAR_MINUSMINUS :
                //<step> ::= var '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS_VAR :
                //<step> ::= '++' var
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_VAR_PLUSPLUS :
                //<step> ::= var '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_VAR_PLUSEQ_NUM :
                //<step> ::= var '+=' num
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_VAR_MINUSEQ_NUM :
                //<step> ::= var '-=' num
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_LOOP_WHILE_LPAREN_RPAREN :
                //<while_loop> ::= while '(' <cond> ')' <compound_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DO_WHILE_DO_WHILE_LPAREN_RPAREN :
                //<do_while> ::= do <compound_stmt> while '(' <cond> ')'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" in line: " + args.UnexpectedToken.Location.LineNr;
            errorViewer.Items.Add(message);
            string expectedToken = "Expected Token: " + args.ExpectedTokens.ToString();
            errorViewer.Items.Add(expectedToken);
        }

        private void TokenReadEvent(LALRParser parser,TokenReadEventArgs args)
        {
            string info = args.Token.Text + "    \t \t" + (SymbolConstants)args.Token.Symbol.Id;
            tokenViewer.Items.Add(info);
        }

    }
}
