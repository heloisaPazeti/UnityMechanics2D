# Dialog System With Options Using Ink

This dialog system differs from the previous Dialog System project (see references) in that it now has options within the dialogs, making it possible to create different branches for the project and elevate the narrative.

In this project Ink files are used to create the texts and their options that are transformed into JSON and then passed to the Dialog Box and to the Options boxes.

## What's Ink?

Ink is inkle's scripting language for writing interactive narrative, both for text-centric games as well as more graphical games that contain highly branching stories.

## How It Works

### Dialog Manager

This script is the one that controls most of the UI, switching screen components as you enter and exit dialogs. It also controls the organization of the options, which ones are shown, and their texts.

### Dialog Trigger

This script is the one that triggers the dialog. In this case, there is a function that is called by clicking the Start button that starts the dialog. It is also the one that stores the JSON file that will be shown in the dialog box. So each object that will have a different interaction can have a different file.

# Sistema de Diálogo Utilizando Ink

Esse sistema de diálogo difere do projeto anterior Dialog System (olhar referências) por ter agora opções dentro dos diálogos sendo possível criar diversas ramificações para o projeto e elevar a narrativa. 

Nesse projeto é utilizado arquivos Ink para criar os textos e suas opções que são transformados em JSON e então são passados para a Caixa de Diálogo e para as caixas de Opções.

## O que é Ink?

Ink é a linguagem de script do inkle para escrever narrativa interativa, tanto para jogos centrados em texto quanto para jogos mais gráficos que contêm histórias altamente ramificadas.

## Como funciona

### Dialog Manager

Esse script é aquele que controla a maior parte da UI, trocando os componentes da tela conforme entra-se e sai de diálogos. Além disso controla a organização das opções, quais são mostradas, e os seus textos.

### Dialog Trigger

Esse script é aquele que aciona o diálogo. Nesse caso possui a função que é chamada pelo clique do botão Start que inicia o diálogo. Também é aquele que armazena o arquivo JSON que será mostrado na caixa de diálogo. Assim cada objeto que terá uma interação diferente poderá ter um arquivo diferente.

##

### <p align="center"> References / Referências</p>

<p align="center"><a href="https://www.youtube.com/watch?v=vY0Sk93YUhA&list=WL&index=68&t=716s">Youtube Tutorial</a></p>
<p align="center"><a href="https://github.com/trevermock/ink-dialogue-system">Original Project</a></p>
<p align="center"><a href="https://www.inklestudios.com/">Inkle Website</a></p>
<p align="center"><a href="https://www.inklestudios.com/ink/">Inkle's Ink Editor</a></p>
<p align="center"><a href="https://github.com/heloisaPazeti/UnityMechanics2D/tree/main/Dialog%20System">Only Text Dialog System</a></p>

